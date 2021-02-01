import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

import { AuthService } from './auth.service';

@Component({
  templateUrl: './login.component.html'
})
export class LoginComponent {
  errorMessage: string;
  pageTitle = 'Log In';

  constructor(
    private authService: AuthService,
    private route: Router) { }

  login(loginForm: NgForm): void {
    if (loginForm?.valid) {
      const userName = loginForm.form.value.userName;
      const password = loginForm.form.value.password;
      this.authService.login(userName, password);

      const redirectUrl: string = this.authService.redirectUrl || '/products';
      this.route.navigate([redirectUrl]);

      // if (this.authService.redirectUrl) {
      //   this.route.navigate([this.authService.redirectUrl]);
      // } else {
      //   this.route.navigate(['/products']);
      // }
    } else {
      this.errorMessage = 'Please enter a user name and password.';
    }
  }
}
