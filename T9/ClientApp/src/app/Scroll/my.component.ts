import { DataService } from './data.service';
import { Component, OnInit } from '@angular/core';
import { TableVirtualScrollDataSource } from 'ng-table-virtual-scroll';
import { catchError, tap, map } from 'rxjs/operators';
import { EmployeeDetail } from './EmployeeDetail';

@Component({
  selector: 'app-my',
  templateUrl: './my.component.html',
  styleUrls: ['./my.component.css']
})
export class MyComponent implements OnInit {
  // dataSource: TableVirtualScrollDataSource<EmployeeDetail[]>;

  // displayedColumns = ['empNo', 'firstName', 'lastName', 'title'];
  // empNo: number;
  // firstName: string;
  // lastName: string;
  // // birthDate: string;
  // // hireDate: string;
  // // gender: string;
  // // salary: number;
  // title: string;
  // // deptNo: string;
  // // deptName: string;
  // // managerEmpNo: number;
  // // managerFirstName: string;
  // // managerLastName: string;

  employeeDetails = [];
  rows: any;
  columns: any;

  constructor(private dataService: DataService) { }

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

    this.dataService.getEmployeeDetails()
      // .pipe(
      //   tap(data => console.log(data.length))
      // )
      .subscribe(
        data => {
          this.rows = data;
        }
    );

    // this.dataService.getEmployeeDetails()
    //   .pipe(
    //     tap(data => console.log(JSON.stringify(data.length)))
    //   )
    //   .subscribe(
    //     data => this.employeeDetails = data
    //     // stream => {
    //     //   this.dataSource.data = new TableVirtualScrollDataSource<EmployeeDetail>(stream);
    //     // }
    //   );

    // this.dataService.getEmployeeDetails()
    // .subscribe(
    //   stream => {
    //     this.dataSource = new TableVirtualScrollDataSource(stream);
    //   }
    // );
  }
}
