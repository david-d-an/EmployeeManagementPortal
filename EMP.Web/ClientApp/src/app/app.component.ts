import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SpinnerService } from './shared/spinner.service';
// import { DepartmentListComponent } from './departments/list/department-list.component';
// import { EmployeeListComponent } from './employees/list/employee-list.component';

@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.css'],
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  pageTitle = 'Employee Portal';

  get isLoading(): boolean {
    return this.spinnerService.isLoading();
  }

  constructor(
    private router: Router,
    private spinnerService: SpinnerService) {
  }
}
