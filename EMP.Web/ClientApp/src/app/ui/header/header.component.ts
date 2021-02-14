import { AuthService } from 'src/app/core/security/auth.service';
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
    });

    this.authService.loginChanged.subscribe(loggedIn => {
      console.log(`set by obs: ${loggedIn}`);
      this.isLoggedIn = loggedIn;
    });
  }

  login() {
    this.authService.login();
  }

  logOut(): void {
    this.authService.logout();
  }
}
