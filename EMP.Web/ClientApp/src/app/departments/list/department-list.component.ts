import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ModalManager } from 'ngb-modal';
import { DepartmentFilter } from 'src/app/models/DepartmentDetail';

import { DepartmentService } from '../service/department.service';
import { SpinnerService } from './../../shared/spinner.service';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.css']
})
export class DepartmentListComponent implements OnInit {
@ViewChild('filterModal') filterModal;

  private modalRef;
  pageTitle = 'Department List';
  rows: any;
  columns: any;
  isLoading = false;
  filterItems: any[];
  currentFilter: DepartmentFilter;
  lastFilter: DepartmentFilter;

  constructor(
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

  ngOnInit(): void {
  }

  loadData(): void {
    this.spinnerService.startLoading();
    this.departmentService.getDepartmentDetails(this.currentFilter)
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
