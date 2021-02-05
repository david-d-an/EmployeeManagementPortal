import { EmployeeFilterModalComponent } from './forms/employee-filter-modal.component';
import { EmployeeFilter, EmployeeFilterAnnotation } from 'src/app/models/EmployeeDetail';
import { AfterViewChecked, Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
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
export class EmployeeListComponent implements OnInit, AfterViewInit, AfterViewChecked {
  // @ViewChild('filterModal') filterModal;
  @ViewChild(EmployeeFilterModalComponent) filterModalComponent: EmployeeFilterModalComponent;

  // private modalRef;
  pageTitle = 'Employee List';
  rows: any;
  columns: any;
  isLoading = false;
  greatherThanOrEqualTo = '\u2267';
  lessThanOrEqualTo = '\u2266';
  filterItems: any[];
  // currentFilter: EmployeeFilter;

  // filterFormGroup = this.formBuilder.group({
  //   firstName: [ '' ],
  //   lastName: [ '' ],
  //   title: [ '' ],
  //   deptName: [ '' ],
  //   salaryMin: [ '', [
  //     Validators.maxLength(10),
  //     positiveNumber
  //   ]],
  //   salaryMax: [ '', [
  //     Validators.maxLength(10),
  //     positiveNumber
  //   ]],
  // }, {
  //   validators: [
  //     salaryMinLessThanSalaryMax
  // ]});


  constructor(
    private employeeService: EmployeeService,
    // private formBuilder: FormBuilder,
    // private modalService: ModalManager,
    private spinnerService: SpinnerService,
    private route: ActivatedRoute,
    private router: Router) {

      // this.columns = [
      //   { prop: 'empNo', name: 'Employee ID' } ,
      //   { prop: 'firstName', name: 'First Name' },
      //   { prop: 'lastName', name: 'Last Name' },
      //   { prop: 'title', name: 'Title' },
      //   { prop: 'salary', name: 'Salary' },
      //   { prop: 'deptNo', name: 'Dept No.' },
      //   { prop: 'deptName', name: 'Department' },
      // ];
  }

    ngOnInit(): void {
      // const userDeptName = 'customer service';
      // // this.initializeDeptNameFilter(userDeptName);
      // console.log(this.filterModalComponent);
      // this.filterModalComponent.initializeDeptNameFilter(userDeptName);
      // this.loadData();
    }

    ngAfterViewInit() {
      const userDeptName = 'customer service';
      // this.initializeDeptNameFilter(userDeptName);
      // console.log(this.filterModalComponent);
      this.filterModalComponent.initializeDeptNameFilter(userDeptName);
      this.runFilter();
      this.loadData();
    }

    ngAfterViewChecked(): void {
      // console.log(`filterFormGroup valid: ${this.filterFormGroup.valid}`);
    }

    // initializeDeptNameFilter(deptName: string) {
    //   this.filterFormGroup.patchValue({
    //     deptName: deptName
    //   });
    //   this.runFilter();
    // }

    applyFilterhandler() {
      setTimeout(() => {
        this.filterItems = this.filterModalComponent.filterItems;
      });
      this.loadData();
    }

    loadData(): void {
      this.spinnerService.startLoading();
      // console.log(this.filterModalComponent.currentFilter);
      this.employeeService.getEmployeeDetails(this.filterModalComponent.currentFilter)
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
      // if (event.type === 'click') {
      //   console.log(event.row);
      //   console.log(`Employee ID: ${event.row.empNo}`);
      // }
    }

    openModal(): void {
      this.filterModalComponent.openModal();
      // this.modalRef = this.modalService.open(this.filterModal, {
      //     size: 'md',
      //     modalClass: '',
      //     hideCloseButton: false,
      //     centered: false,
      //     backdrop: 'static',
      //     keyboard: false,
      //     animation: true,
      //     closeOnOutsideClick: true,
      //     backdropClass: 'modal-backdrop'
      // });
    }

    // applyFilter() {
    //   // this.modalService.close(this.modalRef);
    //   this.filterModalComponent.closeModal();
    //   this.runFilter();
    //   this.loadData();
    // }

    // closeModal() {
    //   // this.modalService.close(this.modalRef);
    //   // this.filterFormGroup.patchValue(this.currentFilter);
    //   this.filterModalComponent.closeModal();
    // }

    runFilter(): void {
      // this.currentFilter = this.filterFormGroup.getRawValue();
      // this.filterItems = [];
      // const f = this.filterModalComponent.currentFilter;

      // Object.keys(this.filterModalComponent.filterFormGroup.controls).forEach(key => {
      //   if (f[key]) {
      //     this.filterItems.push({
      //       'key': key,
      //       'value': `${EmployeeFilterAnnotation.get(key)}: ${f[key]}`
      //     });
      //   }
      // });
      this.filterModalComponent.updateFilterTags();
      setTimeout(() => {
        this.filterItems = this.filterModalComponent.filterItems;
      });
    }

    removeFilter(idx: number): void {
      // const key = this.filterItems[idx].key;
      // this.filterItems.splice(idx, 1);

      // this.currentFilter[key] = '';
      // this.filterFormGroup.controls[key].patchValue('');
      this.filterModalComponent.removeFilter(idx);
      setTimeout(() => {
        this.filterItems = this.filterModalComponent.filterItems;
      });
      this.loadData();
    }

    deleteUser(row): void {
      console.log(`Delete User initiated for Employee ID: ${row.empNo}`);
    }

    editUser(row): void {
      console.log(`Edit User initiated for Employee ID: ${row.empNo}`);
    }
}
