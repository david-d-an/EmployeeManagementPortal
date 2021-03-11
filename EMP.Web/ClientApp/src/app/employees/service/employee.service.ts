import { AuthService } from 'src/app/user/auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from, Observable, of, throwError } from 'rxjs';
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

  constructor(
    private http: HttpClient,
    private authService: AuthService) { }

  getSvcUrlForAll(filter: EmployeeFilter, refresh?: boolean) {
    return this.urlForAll +
      `&firstName=${filter.firstName}` +
      `&lastName=${filter.lastName}` +
      `&deptName=${filter.deptName}` +
      `&title=${filter.title}` +
      `&salaryMin=${filter.salaryMin}` +
      `&salaryMax=${filter.salaryMax}` +
      (refresh ? `&currTime=${Date.now()}` : '');
  }

  getSvcUrlForOne(empNo?: string) {
    if (!empNo) {
      return this.urlForOne ;
    }
    return this.urlForOne + `/${empNo}`;
  }

  getEmployeeDetails(filter: EmployeeFilter, refresh?: boolean): Observable<EmployeeDetail[]> {
    // return this.http.get<EmployeeDetail[]>(this.getSvcUrlForAll(filter))
    //   .pipe(
    //     // tap(data => console.log(JSON.stringify(data))),
    //     catchError(this.handleError)
    //   );
    return from(this.authService.getAccessToken().then(token => {
      return this.http
        .get<EmployeeDetail[]>(this.getSvcUrlForAll(filter, refresh))
        .pipe(
          // tap(data => console.log(JSON.stringify(data))),
          catchError(this.handleError)
        )
        .toPromise();
    }));
  }

  getOneEmployeeDetails(empNo: string): Observable<EmployeeDetail[]> {
    return from(this.authService.getAccessToken().then(token => {
      return this.http.get<EmployeeDetail[]>(this.getSvcUrlForOne(empNo))
      .pipe(
        // tap(data => console.log(JSON.stringify(data))),
        catchError(this.handleError)
      )
      .toPromise();
    }));
  }

  putOneEmployeeDetails(empNo: string, updatedEmployeeData: any): Observable<any> {
    // console.log(`PUTing to Url: ${this.getSvcUrlForOne(empNo)}`);
    // console.log(JSON.stringify(updatedEmployeeData));
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    console.log(`PUTing in Service: ${JSON.stringify(updatedEmployeeData)}`);
    return this.http
      .put(this.getSvcUrlForOne(empNo),
        updatedEmployeeData, { headers: headers} )
      .pipe(
        tap(data => console.log(`updating: ${data}`)),
        catchError(this.handleError)
      );
  }

  postOneEmployeeDetails(empNo: string, employeeDataPayload: any): Observable<any> {
    // console.log(`POSTing to Url: ${this.getSvcUrlForOne()}`);
    // console.log(JSON.stringify(updatedEmployeeData));
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    console.log(`POSTing in Service: ${JSON.stringify(employeeDataPayload)}`);
    return this.http
      .post(this.getSvcUrlForOne(),
        employeeDataPayload, { headers: headers} )
      .pipe(
        tap(data => console.log(`creating: ${data}`)),
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
