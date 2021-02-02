import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { EmployeeService } from '../service/employee.service';
import { SpinnerService } from './../../shared/spinner.service';

import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employeeDetails = [];
  rows: any;
  columns: any;
  isLoading = false;

  constructor(
    private employeeService: EmployeeService,
    private spinnerService: SpinnerService,
    private route: ActivatedRoute,
    private router: Router) { }

    ngOnInit(): void {

      this.columns = [
        { prop: 'empNo', name: 'Employee ID' } ,
        { prop: 'firstName', name: 'First Name' },
        { prop: 'lastName', name: 'Last Name' },
        { prop: 'title', name: 'Title' },
        { prop: 'salary', name: 'Salary' },
        { prop: 'deptNo', name: 'Dept No.' },
        { prop: 'deptName', name: 'Department' },
      ];

      this.spinnerService.startLoading();
      this.employeeService.getEmployeeDetails()
        // .pipe(
        //   tap(data => console.log(data.length))
        // )
        .subscribe({
          next: data => {
            this.rows = data;
              this.spinnerService.stopLoading();
          },
          error: error => {
              this.spinnerService.stopLoading();
            console.log(error);
          }
        });
    }
}
