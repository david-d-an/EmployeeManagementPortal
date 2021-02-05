import { NgModule } from '@angular/core';
import { EmpCommonModule } from '../shared/emp-common.module';
import { EmpStyleModule } from './../shared/emp-style.module';
import { EmployeesRoutingModule } from './employees-routing.module';
import { ReactiveFormsModule } from '@angular/forms';

import { EmployeeListComponent } from './list/employee-list.component';
import { EmployeeDetailComponent } from './detail/employee-detail.component';
import { EmployeeEditComponent } from './edit/employee-edit.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { EmployeeFilterModalComponent } from './list/forms/employee-filter-modal.component';
import { EmployeeEditModalComponent } from './list/forms/employee-edit-modal.component';

@NgModule({
  declarations: [
    EmployeeListComponent,
    EmployeeDetailComponent,
    EmployeeEditComponent,
    EmployeeFilterModalComponent,
    EmployeeEditModalComponent,
    EmployeeFilterModalComponent,
    EmployeeEditModalComponent
  ],
  imports: [
    EmpCommonModule,
    EmployeesRoutingModule,
    EmpStyleModule,
    NgxDatatableModule,
    ReactiveFormsModule
  ]
})
export class EmployeesModule { }
