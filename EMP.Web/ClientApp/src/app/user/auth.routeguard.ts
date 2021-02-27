import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, RouterStateSnapshot, UrlSegment, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({providedIn: 'root'})
export class AuthRouteGuard implements CanActivate, CanLoad {
  constructor(private authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      sessionStorage['originalUrl'] = window.location.href;
      return this.authService.initAuthSession().then(s => {
        return s;
      });
  }

  canLoad(route: Route, segments: UrlSegment[]):
    Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      sessionStorage['originalUrl'] = window.location.href;
      return this.authService.initAuthSession().then(s => {
        return s;
      });
  }
}
