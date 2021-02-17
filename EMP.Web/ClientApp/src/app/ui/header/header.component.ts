import { AuthService } from 'src/app/user/auth.service';
import { AfterViewChecked, Component, OnInit, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean;

  constructor(private authService: AuthService) {
    this.isLoggedIn = false;
  }

  ngOnInit() {
    this.authService.isLoggedIn().then(loggedIn => {
      console.log(`set by promise: ${loggedIn}`);
      this.isLoggedIn = loggedIn;

      if (loggedIn) {
        sessionStorage.removeItem('authChecked');
        // To Do:
        // authService.
      } else if (!sessionStorage['authChecked'] && !loggedIn) {
        sessionStorage['authChecked'] = true;
        this.authService.preCheckAuthSession();
      }
    });

    this.authService.loginChanged.subscribe(loggedIn => {
      console.log(`set by obs: ${loggedIn}`);
      this.isLoggedIn = loggedIn;

      if (loggedIn) {
        sessionStorage.removeItem('authChecked');
      } else if (!sessionStorage['authChecked'] && !loggedIn) {
        sessionStorage['authChecked'] = true;
        this.authService.preCheckAuthSession();
      }
    });
  }

  login() {
    this.authService.login();
  }

  logOut(): void {
    this.authService.logout();
  }
}
