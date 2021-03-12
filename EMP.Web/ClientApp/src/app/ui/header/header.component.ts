import { AuthService } from 'src/app/user/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean;
  // userName: string;
  // userRoles: string;

  get userName() {
    return this.authService.userName;
  }
  get userRole() {
    return this.authService.userRole;
  }

  constructor(
    private location: Location,
    private router: Router,
    private authService: AuthService) {
    this.isLoggedIn = false;
  }

  setLoggedIn(loggedIn: boolean|null) {
    this.isLoggedIn = loggedIn;

    // console.log(`loggedIn: ${loggedIn}`);
    // console.log(`!loggedIn: ${!loggedIn}`);
    // console.log(`!sessionStorage['authPreChecked']: ${!sessionStorage['authPreChecked']}`);
    // console.log(`sessionStorage['forceLogin_EMP.Web']: ${sessionStorage['forceLogin_EMP.Web']}`);
    // console.log(`sessionStorage['forceLogout_EMP.Web']: ${sessionStorage['forceLogout_EMP.Web']}`);
    // console.log(`sessionStorage['originalUrl']: ${sessionStorage['originalUrl']}`);
    if (loggedIn) {
      if (sessionStorage['forceLogout_EMP.Web']) {
        this.logOutThisTabOnly();
      }
    } else if (!loggedIn) {
      if (sessionStorage['forceLogin_EMP.Web']) {
        sessionStorage.removeItem('forceLogin_EMP.Web');
        sessionStorage['authPreChecked'] = true;
        this.authService.preCheckAuthSession(window.location.href);
      } else if (!sessionStorage['authPreChecked']) {
        sessionStorage['authPreChecked'] = true;
        this.authService.preCheckAuthSession(window.location.href);
      }
    }
  }

  ngOnInit() {
    this.refreshCookie();

    window.addEventListener('storage', (event) => {
      if (event.key === 'forceLogout_EMP.Web_Shared' && !!event.newValue) {
        sessionStorage['forceLogout_EMP.Web'] = true;
        sessionStorage.removeItem('forceLogin_EMP.Web');
      } else if (event.key === 'forceLogin_EMP.Web_Shared' && !!event.newValue) {
        sessionStorage['forceLogin_EMP.Web'] = true;
        sessionStorage.removeItem('forceLogout_EMP.Web');
      }
    });

    this.authService.loginChanged.subscribe(loggedIn => {
      console.log(`loggedIn set by obs: ${loggedIn}`);
      this.setLoggedIn(loggedIn);
      if (!loggedIn) {
        for (const key in sessionStorage) {
          // 'oidc.user:https://localhost:5500/:emp-web-client'
          if (key.indexOf('oidc.user:') !== -1
          && key.indexOf(':emp-web-client') !== -1) {
            sessionStorage.removeItem(key);
          }
        }
        this.router.navigate(['/home']);
      }
    });

    if (!this.isLoggedIn) {
      this.authService.isLoggedIn().then(loggedIn => {
        console.log(`loggedIn set by promise: ${loggedIn}`);
        this.setLoggedIn(loggedIn);
      });
    }
  }

  refreshCookie() {
    this.authService.getUser().then(data => {
      console.log('Cookie refreshed');
      console.log(JSON.stringify(data));
    });
  }

  login(): void {
    localStorage['forceLogin_EMP.Web_Shared'] = true;
    sessionStorage['originalUrl'] = window.location.href;
    this.authService.login();
    localStorage.removeItem('forceLogin_EMP.Web_Shared');
  }

  logOut(): void {
    // Use localStorage to support multi-tab sign out
    localStorage['forceLogout_EMP.Web_Shared'] = true;
    this.logOutThisTabOnly();
    localStorage.removeItem('forceLogout_EMP.Web_Shared');
  }

  logOutThisTabOnly(): void {
    console.log('logOutThisTabOnly');
    sessionStorage.removeItem('forceLogout_EMP.Web');
    // sessionStorage.removeItem('authPreChecked');
    this.authService.logout();
    if (this.location.path() !== '/home') {
      this.router.navigate(['/home']);
    }
  }
}
