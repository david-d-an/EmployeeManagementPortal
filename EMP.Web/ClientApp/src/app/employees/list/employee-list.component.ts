import { EmployeeFilter } from './../../models/EmployeeDetail';
import { AfterViewChecked, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalManager } from 'ngb-modal';
import { tap } from 'rxjs/operators';

import { EmployeeService } from '../service/employee.service';
import { SpinnerService } from './../../shared/spinner.service';
import { FormBuilder, Validators } from '@angular/forms';
import { positiveNumber, salaryMinLessThanSalaryMax } from 'src/app/Validation/Validators';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit, AfterViewChecked {
  @ViewChild('filterModal') filterModal;

  private modalRef;
  pageTitle = 'Employee List';
  rows: any;
  columns: any;
  isLoading = false;
  greatherThanOrEqualTo = '\u2267';
  lessThanOrEqualTo = '\u2266';
  filterItems: any[];
  currentFilter: EmployeeFilter;
  lastFilter: EmployeeFilter;

  filterFormGroup = this.formBuilder.group({
    firstName: [ '' ],
    lastName: [ '' ],
    title: [ '' ],
    deptName: [ '' ],
    salaryMin: [ '', [
      Validators.maxLength(10),
      positiveNumber
    ]],
    salaryMax: [ '', [
      Validators.maxLength(10),
      positiveNumber
    ]],
  }, {
    validators: [
      salaryMinLessThanSalaryMax
  ]});


  constructor(
    private formBuilder: FormBuilder,
    private employeeService: EmployeeService,
    private modalService: ModalManager,
    private spinnerService: SpinnerService,
    private route: ActivatedRoute,
    private router: Router) {

      this.columns = [
        { prop: 'empNo', name: 'Employee ID' } ,
        { prop: 'firstName', name: 'First Name' },
        { prop: 'lastName', name: 'Last Name' },
        { prop: 'title', name: 'Title' },
        { prop: 'salary', name: 'Salary' },
        { prop: 'deptNo', name: 'Dept No.' },
        { prop: 'deptName', name: 'Department' },
      ];

      this.lastFilter = {
        empNo: '',
        firstName: '',
        lastName: '',
        salaryMin: '',
        salaryMax: '',
        title: '',
        deptName: ''
      };
      this.currentFilter = { ... this.lastFilter };
  }

    ngOnInit(): void {
      this.initializeDeptNameFilter('customer service');
      this.loadData();
    }

    ngAfterViewChecked(): void {
      // console.log(`filterFormGroup valid: ${this.filterFormGroup.valid}`);
    }

    initializeDeptNameFilter(deptName: string) {
      this.filterFormGroup.patchValue({
        deptName: deptName
      });
      this.runFilter();
    }

    loadData(): void {
      this.spinnerService.startLoading();
      this.employeeService.getEmployeeDetails(this.currentFilter)
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
      this.modalRef = this.modalService.open(this.filterModal, {
          size: 'md',
          modalClass: '',
          hideCloseButton: false,
          centered: false,
          backdrop: 'static',
          keyboard: false,
          animation: true,
          closeOnOutsideClick: true,
          backdropClass: 'modal-backdrop'
      });
    }

    applyFilter() {
      this.modalService.close(this.modalRef);
      this.runFilter();
      this.loadData();
    }

    closeModal() {
      this.modalService.close(this.modalRef);
      this.currentFilter = { ... this.lastFilter };
    }

    runFilter(): void {

      this.currentFilter = {
        empNo: '',
        firstName: this.filterFormGroup.value.firstName,
        lastName: this.filterFormGroup.value.lastName,
        salaryMin: this.filterFormGroup.value.salaryMin,
        salaryMax: this.filterFormGroup.value.salaryMax,
        title: this.filterFormGroup.value.title,
        deptName: this.filterFormGroup.value.deptName
      };

      this.lastFilter = { ... this.currentFilter };

      this.filterItems = [];
      const f = this.currentFilter;

      if (f.firstName) {
        this.filterItems.push({
          'key': 'firstName',
          'value': `First Name: ${f.firstName}`
        });
      }
      if (f.lastName) {
        this.filterItems.push({
          'key': 'lastName',
          'value': `Last Name: ${f.lastName}`
        });
      }
      if (f.title) {
        this.filterItems.push({
          'key': 'title',
          'value': `Title: ${f.title}`
        });
      }
      if (f.deptName) {
        this.filterItems.push({
          'key': 'deptName',
          'value': `Department: ${f.deptName}`
        });
      }
      if (f.salaryMin) {
        this.filterItems.push({
          'key': 'salaryMin',
          'value': `Salary ${this.greatherThanOrEqualTo} ${f.salaryMin}`
        });
      }
      if (f.salaryMax) {
        this.filterItems.push({
          'key': 'salaryMax',
          'value': `Salary ${this.lessThanOrEqualTo} ${f.salaryMax}`
        });
      }
    }

    removeFilter(idx: number): void {
      const key = this.filterItems[idx].key;
      this.filterItems.splice(idx, 1);

      this.currentFilter[key] = '';
      this.lastFilter[key] = '';

      this.loadData();
    }

    deleteUser(v): void {
    }

    editUser(v): void {
    }
}
