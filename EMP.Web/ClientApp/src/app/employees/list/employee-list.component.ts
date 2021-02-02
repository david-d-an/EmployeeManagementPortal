import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalManager } from 'ngb-modal';
import { tap } from 'rxjs/operators';

import { EmployeeService } from '../service/employee.service';
import { SpinnerService } from './../../shared/spinner.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
@ViewChild('myModal') myModal;

  private modalRef;
  pageTitle = 'Employee List';
  employeeDetails = [];
  rows: any;
  columns: any;
  isLoading = false;

  constructor(
    private employeeService: EmployeeService,
    private modalService: ModalManager,
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

    onActivate(event: any): void {
      if (event.type === 'click') {
        console.log(event.row);
        console.log(`Employee ID: ${event.row.empNo}`);
      }
    }

    openModal(): void {
      this.modalRef = this.modalService.open(this.myModal, {
          size: 'md',
          modalClass: 'mymodal',
          hideCloseButton: false,
          centered: false,
          backdrop: true,
          animation: true,
          keyboard: false,
          closeOnOutsideClick: true,
          backdropClass: 'modal-backdrop'
      });
    }

    closeModal(){
        this.modalService.close(this.modalRef);
    }

    foo(): void { }

}
