import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { DepartmentDetail, DepartmentFilter } from 'src/app/models/DepartmentDetail';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  pageNum = 1;
  pageSize = 12;
  svcUrlBase = `https://localhost:15001/api/EmployeeDetail?pageNum=${this.pageNum}&pageSize=${this.pageSize}`;

  constructor(private http: HttpClient) { }

  getSvcUrl(filter: DepartmentFilter) {
    return this.svcUrlBase +
      `&firstName=${filter.firstName}` +
      `&lastName=${filter.lastName}` +
      `&deptName=${filter.deptName}` +
      `&title=${filter.title}` +
      `&salaryMin=${filter.salaryMin}` +
      `&salaryMax=${filter.salaryMax}`;
  }

  getDepartmentDetails(filter: DepartmentFilter): Observable<DepartmentDetail[]> {
    return this.http.get<DepartmentDetail[]>(this.getSvcUrl(filter))
      .pipe(
        // tap(data => console.log(JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  private handleError(err: any): Observable<never> {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      // errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
      errorMessage = `Backend returned Error ${err.status}: ${err}`;
    }
    console.log('error detected');
    console.error(err);
    return throwError(errorMessage);
  }
}
