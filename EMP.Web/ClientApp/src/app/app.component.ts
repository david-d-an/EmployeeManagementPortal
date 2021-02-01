import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.css'],
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  pageTitle = 'Employee Portal';
  loading: boolean;
  // textcolor: string;

  constructor(private router: Router) {
    this.loading = false;
  }

  // changeColor() {
  //   this.textcolor = 'red';
  // }

  // returnColor() {
  //   this.textcolor = 'black';
  // }
}
