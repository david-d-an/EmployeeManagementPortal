import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { EmployeeDetail } from './EmployeeDetail';

// const httpOptions = {
//   headers: new HttpHeaders({
//     'Content-Type': 'application/json',
//     'Access-Control-Allow-Origin': '*',
//   })
// };

// const headers= new HttpHeaders()
//   .set('content-type', 'application/json')
//   .set('Access-Control-Allow-Origin', '*');

@Injectable({
  providedIn: 'root'
})
export class DataService {

  svcUrl = 'https://localhost:15001/api/EmployeeDetail';

  constructor(private http: HttpClient) {

  }

  getEmployeeDetails(): Observable<EmployeeDetail[]> {
    // return this.http.get<EmployeeDetails[]>(this.svcUrl, { 'headers': httpOptions.headers })
    return this.http.get<EmployeeDetail[]>(this.svcUrl)
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
