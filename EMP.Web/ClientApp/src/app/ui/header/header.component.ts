import { AuthService } from 'src/app/user/auth.service';
import { AfterViewChecked, Component, OnInit, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean;

  constructor(
    private authService: AuthService) {
    this.isLoggedIn = false;
  }

  ngOnInit() {

    // localStorage event listner to support multi tab sign out
    window.addEventListener('storage', (event) => {
      if (event.key === 'forceLogout_EMP.Web_Shared' && !!event.newValue) {
        sessionStorage['forceLogout_EMP.Web'] = true;
      }
    });

    this.authService.isLoggedIn().then(loggedIn => {
      console.log(`set by promise: ${loggedIn}`);
      this.isLoggedIn = loggedIn;

      // console.log(`loggedIn: ${loggedIn}`);
      // console.log(`!loggedIn: ${!loggedIn}`);
      // console.log(`!sessionStorage['authPreChecked']: ${!sessionStorage['authPreChecked']}`);
      if (loggedIn) {
        if (sessionStorage['forceLogout_EMP.Web']) {
          this.logOutThisTabOnly();
        }
      } else if (!loggedIn && !sessionStorage['authPreChecked']) {
        sessionStorage['authPreChecked'] = true;
        this.authService.preCheckAuthSession();
      }
    });

    this.authService.loginChanged.subscribe(loggedIn => {
      console.log(`set by obs: ${loggedIn}`);
      this.isLoggedIn = loggedIn;

      // console.log(`loggedIn: ${loggedIn}`);
      // console.log(`!loggedIn: ${!loggedIn}`);
      // console.log(`!sessionStorage['authPreChecked']: ${!sessionStorage['authPreChecked']}`);
      if (loggedIn) {
        console.log(`sessionStorage['forceLogout_EMP.Web']: ${sessionStorage['forceLogout_EMP.Web']}`);
        if (sessionStorage['forceLogout_EMP.Web']) {
          this.logOutThisTabOnly();
        }
      } else if (!loggedIn && !sessionStorage['authPreChecked']) {
        sessionStorage['authPreChecked'] = true;
        this.authService.preCheckAuthSession();
      }
    });
  }

  login(): void {
    this.authService.login();
  }

  logOut(): void {
    // Use localStorage to support multi-tab sign out
    localStorage['forceLogout_EMP.Web_Shared'] = true;
    this.logOutThisTabOnly();
    localStorage.removeItem('forceLogout_EMP.Web_Shared');
  }

  logOutThisTabOnly(): void {
    this.authService.logout();
    sessionStorage.removeItem('authPreChecked');
    sessionStorage.removeItem('forceLogout_EMP.Web');
  }
}
