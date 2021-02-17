import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from, Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from 'src/app/user/auth.service';
import { AppConfig } from '../app.config';

@Injectable({
  providedIn: 'root'
})
export class ApiAuthService {

  get urlForAll(): string {
    return `${AppConfig.settings.apiServer.employees}/api/EmployeeDetail`;
  }
  get urlForOne(): string {
    return `${AppConfig.settings.apiServer.employees}/api/EmployeeDetail`;
  }

  constructor(
    private http: HttpClient,
    private authService: AuthService) { }

  getEmployeeDetails(): void {
    const x = from(this.authService.getAccessToken().then(token => {
      // Find User Name or User Email and check
      // against /api/UserPrfile/{username}
      // if 200 OK: Skip
      // else if Denied 401: erase sessionStorage, change isLoggedIn

      // return this.http
      //   .get<EmployeeDetail[]>(this.getSvcUrlForAll(filter))
      //   .pipe(
      //     // tap(data => console.log(JSON.stringify(data))),
      //     catchError(this.handleError)
      //   )
      //   .toPromise();
    }));
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
