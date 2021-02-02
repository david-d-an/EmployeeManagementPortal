import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ModalManager } from 'ngb-modal';

import { DepartmentService } from '../service/department.service';
import { SpinnerService } from './../../shared/spinner.service';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.css']
})
export class DepartmentListComponent implements OnInit {
@ViewChild('myModal') myModal;

  private modalRef;
  pageTitle = 'Department List';
  filters: string[];
  greatherThanOrEqualTo = '\u2267';
  lessThanOrEqualTo = '\u2266';

  constructor(
    private departmentService: DepartmentService,
    private modalService: ModalManager,
    private spinnerService: SpinnerService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {

    this.filters = [
      `Salary ${this.greatherThanOrEqualTo} 10`,
      `Salary ${this.lessThanOrEqualTo} 1000`,
    ];
  }

  removeFilter(idx: number): void {
    this.filters.splice(idx, 1);
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
