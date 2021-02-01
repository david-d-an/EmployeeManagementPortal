import { Route } from '@angular/compiler/src/core';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, CanActivateChild, CanLoad, UrlSegment } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(private authService: AuthService,
              private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      console.log('CanActivate');
      // console.log('CanActivate: state');
      // console.log(state.ur);
      // console.log('CanActivate: next');
      // console.log(next);

      return this.checkLoggedIn(state.url) ?
        true :
        this.router.navigate(['/login']);
  }

  canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      // console.log('CanActivateChild');
      // console.log('CanActivateChild: state');
      // console.log(state);
      // console.log('CanActivateChild: next');
      // console.log(next);

      return this.checkLoggedIn(state.url) ?
        true :
        this.router.navigate(['/login']);
  }

  canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      // return this.checkLoggedIn(route['path']) ?
      return this.checkLoggedIn('/' + segments.join('/')) ?
        true :
        this.router.navigate(['/login']);
  }



  checkLoggedIn(redirectUrl: string): boolean {
    if (this.authService.isLoggedIn) {
      this.authService.redirectUrl = null;
    } else {
      this.authService.redirectUrl = redirectUrl;
    }
    return this.authService.isLoggedIn;
  }
}
