import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { AppConfig } from 'src/app/app.config';
import { EmployeeDetail, EmployeeEditDetail, EmployeeFilter } from 'src/app/models/EmployeeDetail';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  pageNum = '';
  pageSize = '';

  get urlForAll(): string {
    return `${AppConfig.settings.apiServer.employees}/api/EmployeeDetail` +
           `?pageNum=${this.pageNum}&pageSize=${this.pageSize}`;
  }
  get urlForOne(): string {
    return `${AppConfig.settings.apiServer.employees}/api/EmployeeDetail`;
  }

  constructor(private http: HttpClient) { }

  getSvcUrlForAll(filter: EmployeeFilter) {
    return this.urlForAll +
      `&firstName=${filter.firstName}` +
      `&lastName=${filter.lastName}` +
      `&deptName=${filter.deptName}` +
      `&title=${filter.title}` +
      `&salaryMin=${filter.salaryMin}` +
      `&salaryMax=${filter.salaryMax}`;
  }

  getSvcUrlForOne(empNo: string) {
    return this.urlForOne + `/${empNo}`;
  }

  getEmployeeDetails(filter: EmployeeFilter): Observable<EmployeeDetail[]> {
    return this.http.get<EmployeeDetail[]>(this.getSvcUrlForAll(filter))
      .pipe(
        // tap(data => console.log(JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  getOneEmployeeDetails(empNo: string): Observable<EmployeeDetail[]> {
    return this.http.get<EmployeeDetail[]>(this.getSvcUrlForOne(empNo))
      .pipe(
        // tap(data => console.log(JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  putOneEmployeeDetails(empNo: string, updatedEmployeeData: any): Observable<any> {
    // console.log(`PUTing to Url: ${this.getSvcUrlForOne(empNo)}`);
    // console.log(JSON.stringify(updatedEmployeeData));
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http
      .put(this.getSvcUrlForOne(empNo),
        JSON.stringify(updatedEmployeeData), { headers: headers} )
      .pipe(
        tap(data => console.log(`updating: ${data}`)),
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
