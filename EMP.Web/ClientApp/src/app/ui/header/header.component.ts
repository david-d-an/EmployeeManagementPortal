import { AuthService } from 'src/app/core/security/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean;

  constructor(private authService: AuthService) {
    this.isLoggedIn = false;
    // this.authService.loginChanged.subscribe(loggedIn => {
    //   this.isLoggedIn = loggedIn;
    // });
  }

  ngOnInit() {
    this.authService.loginChanged.subscribe(loggedIn => {
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
