import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { from, Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { AppConfig } from 'src/app/app.config';


@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorsService implements HttpInterceptor {

  constructor(private authService: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.url.startsWith('assets/config')) {
      return next.handle(req);
    }

    const apiUrl = AppConfig.settings.apiServer.employees;
    if (req.url.startsWith(apiUrl)) {
      return from(this.authService.getAccessToken().then(token => {
        if (!token) {
          return next.handle(req).toPromise();
        }

        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        const authReq = req.clone({ headers });
        return next.handle(authReq).toPromise();
      }));
    } else {
      return next.handle(req);
    }
  }
}
