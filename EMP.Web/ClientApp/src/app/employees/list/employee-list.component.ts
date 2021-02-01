import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../service/employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employeeDetails = [];
  rows: any;
  columns: any;

  constructor(
    private employeeService: EmployeeService,
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

      this.employeeService.getEmployeeDetails()
        // .pipe(
        //   tap(data => console.log(data.length))
        // )
        .subscribe(
          data => {
            this.rows = data;
          }
      );
    }
}
