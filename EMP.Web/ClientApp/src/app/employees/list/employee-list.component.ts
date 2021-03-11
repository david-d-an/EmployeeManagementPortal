import { EmployeeFilterModalComponent } from './forms/employee-filter-modal.component';
import { EmployeeFilter, EmployeeFilterAnnotation } from 'src/app/models/EmployeeDetail';
import { AfterViewChecked, Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, tap } from 'rxjs/operators';

import { EmployeeService } from '../service/employee.service';
import { SpinnerService } from './../../shared/spinner.service';
import { EmployeeEditModalComponent } from './forms/employee-edit-modal.component';
import { MessageModalComponent } from 'src/app/message-modal/message-modal.component';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit, AfterViewInit, AfterViewChecked {
  @ViewChild(EmployeeFilterModalComponent) filterModalComponent: EmployeeFilterModalComponent;
  @ViewChild(EmployeeEditModalComponent) editModalComponent: EmployeeEditModalComponent;
  @ViewChild(MessageModalComponent) messageModalComponent: MessageModalComponent;

  // private modalRef;
  pageTitle = 'Employee List';
  rows: any;
  columns: any;
  isLoading = false;
  greatherThanOrEqualTo = '\u2267';
  lessThanOrEqualTo = '\u2266';
  filterItems: any[];
  filterChangeSubscription: any;

  constructor(
    private employeeService: EmployeeService,
    private spinnerService: SpinnerService,
    private route: ActivatedRoute,
    private router: Router) {
  }

    ngOnInit(): void {
      // setTimeout(() => {
      //   this.messageModalComponent.openModal('Show this', 'It worked.');
      // });
    }

    ngAfterViewInit() {
      this.filterChangeSubscription = this.filterModalComponent.filterChanged
      .pipe(debounceTime(500))
      .subscribe(f => {
        // console.log(`Filter by Observable: ${JSON.stringify(f)}`);
        this.filterItems = this.filterModalComponent.filterItems;
        this.loadData();
      });

      const userDeptName = 'customer service';
      this.filterModalComponent.initializeDeptNameFilter(userDeptName);
    }

    ngAfterViewChecked(): void {
      // console.log(`filterFormGroup valid: ${this.filterFormGroup.valid}`);
    }

    // decided to go with subscription instead of event emitter
    // applyFilterhandler() {
    //   setTimeout(() => {
    //     this.filterItems = this.filterModalComponent.filterItems;
    //   });
    //   this.loadData();
    // }

    refreshTable(): void {
      this.loadData(true);
      // this.filterModalComponent.updateFilterTags();
    }

    loadData(refresh?: boolean): void {
      this.spinnerService.startLoading();
      this.employeeService.getEmployeeDetails(this.filterModalComponent.currentFilter, refresh)
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
    }

    removeFilter(idx: number): void {
      this.filterModalComponent.removeFilter(idx);
    }

    deleteUser(row): void {
      console.log(`Delete User initiated for Employee ID: ${row.empNo}`);
    }

    editUser(row): void {
      // console.log(`Edit User initiated for Employee ID: ${row.empNo}`);
      this.editModalComponent.openModal(row.empNo);
    }
}
