import { Component, ViewChild,
         OnInit, AfterViewInit, AfterViewChecked, OnChanges, DoCheck,
         AfterContentInit, AfterContentChecked, OnDestroy, Input } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ModalManager } from 'ngb-modal';
import { DepartmentFilter } from 'src/app/models/DepartmentDetail';
import { positiveNumber, salaryMinLessThanSalaryMax } from 'src/app/Validators/Validators';

import { DepartmentService } from '../service/department.service';
import { SpinnerService } from './../../shared/spinner.service';
// import { ValidatorFn } from 'src/app/Validation/Validators';


@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.css']
})
export class DepartmentListComponent implements
  OnChanges,
  OnInit,
  DoCheck,
  AfterContentInit,
  AfterContentChecked,
  AfterViewInit,
  AfterViewChecked,
  OnDestroy {

  @ViewChild('filterModal') filterModal: any;
  @Input() power: string;

  private modalRef;
  pageTitle = 'Department List';
  rows: any;
  columns: any;
  isLoading = false;
  filterItems: any[];
  currentFilter: DepartmentFilter;
  lastFilter: DepartmentFilter;


  checkoutForm = this.formBuilder.group({
    name: ['', Validators.required],
    address: ''
  });

  checkoutForm2 = this.formBuilder.group({
    salaryMin: [ '', [
      positiveNumber
      // Validators.required,
      // Validators.maxLength(3),
      // blue
      ]
    ],
    salaryMax: [ '', [
      positiveNumber
      // Validators.required,
      // Validators.maxLength(3),
      // blue
    ]],
  },
  { validators: salaryMinLessThanSalaryMax });

  onSubmit(): void {
    console.warn('1 - Your order has been submitted', this.checkoutForm.value);
    this.checkoutForm.reset();
  }

  onSubmit2(): void {
    console.warn('2 - Your order has been submitted', this.checkoutForm.value);
    this.checkoutForm.reset();
  }

  constructor(
    private formBuilder: FormBuilder,
    private departmentService: DepartmentService,
    private modalService: ModalManager,
    private spinnerService: SpinnerService,
    private route: ActivatedRoute,
    private router: Router) {

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


  ngOnChanges(): void {
    console.log('OnChanges');
  }

  ngOnInit(): void {
    console.log('OnInit');
  }

  ngDoCheck(): void {
    console.log('DoCheck');
  }

  ngAfterContentInit(): void {
    console.log('AfterContentInit');
  }

  ngAfterContentChecked(): void {
    console.log('AfterContentChecked');
  }

  ngAfterViewInit(): void {
    console.log('AfterViewInit');
  }

  ngAfterViewChecked(): void {
    console.log('AfterViewChecked');
    // console.log(`Form1 valid: ${this.checkoutForm.valid}`);
    console.log(`Form2 valid: ${this.checkoutForm2.valid}`);
  }

  ngOnDestroy(): void {
    // alert('OnDestroy');
    console.log('OnDestroy');
  }


  loadData(): void {
    this.spinnerService.startLoading();
    this.departmentService.getDepartmentDetails()
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

  openModal(): void {
    this.modalRef = this.modalService.open(this.filterModal, {
        size: 'md',
        modalClass: '',
        hideCloseButton: false,
        centered: true,
        backdrop: true,
        animation: true,
        keyboard: true,
        closeOnOutsideClick: true,
        backdropClass: 'modal-backdrop',
    });
  }


  applyFilter() {
    this.modalService.close(this.modalRef);
    this.lastFilter = { ... this.currentFilter };

    this.runFilter();
    this.loadData();
  }

  closeModal() {
    this.modalService.close(this.modalRef);
    this.currentFilter = { ... this.lastFilter };
  }

  runFilter(): void {
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
  }

  removeFilter(idx: number): void {
    this.filterItems.splice(idx, 1);
  }

  onFilterModalOpen(): void { }

  onFilterModalClose(): void { }

}
