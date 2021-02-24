import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/user/auth.service';

@Component({
  selector: 'app-signin-callback',
  template: `<div></div>`
})

export class SigninRedirectCallbackComponent implements OnInit {
  constructor(private authService: AuthService,
              private router: Router) { }

  ngOnInit() {
    this.authService.completeLogin().then(user => {
      if (sessionStorage['originalUrl']) {
        const url = new URL(sessionStorage['originalUrl']);
        sessionStorage.removeItem('originalUrl');
        this.router.navigateByUrl(url.pathname + url.search + url.hash);
      } else {
        this.router.navigate(['/'], { replaceUrl: true });
      }
    });
  }
}
