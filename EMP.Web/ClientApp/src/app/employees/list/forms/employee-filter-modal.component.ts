import { EmployeeFilter, EmployeeFilterAnnotation } from 'src/app/models/EmployeeDetail';
import { AfterViewChecked, Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalManager } from 'ngb-modal';
import { tap } from 'rxjs/operators';

import { EmployeeService } from './../../service/employee.service';
import { SpinnerService } from './../../../shared/spinner.service';
import { FormBuilder, Validators } from '@angular/forms';
import { positiveNumber, salaryMinLessThanSalaryMax } from 'src/app/Validation/Validators';

@Component({
  selector: 'app-employee-filter-modal',
  templateUrl: './employee-filter-modal.component.html',
  styleUrls: ['./employee-filter-modal.component.css']
})
export class EmployeeFilterModalComponent implements OnInit {
  @ViewChild('filterModal') filterModal;
  @Output() applyFilterEvent = new EventEmitter();

  private modalRef;
  pageTitle = 'Employee List';
  rows: any;
  columns: any;
  isLoading = false;
  greatherThanOrEqualTo = '\u2267';
  lessThanOrEqualTo = '\u2266';
  filterItems: any[];
  currentFilter: EmployeeFilter;

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
    private modalService: ModalManager) {
      this.columns = [
        { prop: 'empNo', name: 'Employee ID' } ,
        { prop: 'firstName', name: 'First Name' },
        { prop: 'lastName', name: 'Last Name' },
        { prop: 'title', name: 'Title' },
        { prop: 'salary', name: 'Salary' },
        { prop: 'deptNo', name: 'Dept No.' },
        { prop: 'deptName', name: 'Department' },
      ];

    }

  ngOnInit(): void {
    // this.initializeDeptNameFilter('customer service');
  }

  initializeDeptNameFilter(deptName: string) {
    this.filterFormGroup.patchValue({
      deptName: deptName
    });
    this.updateFilterTags();
    this.applyFilterEvent.next();
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
    this.updateFilterTags();
    this.applyFilterEvent.next();
  }

  closeModal() {
    this.modalService.close(this.modalRef);
    this.filterFormGroup.patchValue(this.currentFilter);
  }

  updateFilterTags(): void {
    this.currentFilter = this.filterFormGroup.getRawValue();
    this.filterItems = [];
    const f = this.currentFilter;

    Object.keys(this.filterFormGroup.controls).forEach(key => {
      if (f[key]) {
        this.filterItems.push({
          'key': key,
          'value': `${EmployeeFilterAnnotation.get(key)}: ${f[key]}`
        });
      }
    });
  }

  removeFilter(idx: number): void {
    const key = this.filterItems[idx].key;
    this.filterItems.splice(idx, 1);

    this.currentFilter[key] = '';
    this.filterFormGroup.controls[key].patchValue('');

    this.applyFilterEvent.next();
  }
}
