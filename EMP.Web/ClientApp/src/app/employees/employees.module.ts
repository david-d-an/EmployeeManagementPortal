import { EmpCommonModule } from '../shared/emp-common.module';
import { EmployeesRoutingModule } from './employees-routing.module';
import { NgModule } from '@angular/core';
// import { CommonModule } from '@angular/common';
import { EmployeeDetailComponent } from './detail/employee-detail.component';
import { EmployeeListComponent } from './list/employee-list.component';
import { EmployeeEditComponent } from './edit/employee-edit.component';



@NgModule({
  declarations: [
    EmployeeListComponent,
    EmployeeDetailComponent,
    EmployeeEditComponent
  ],
  imports: [
    // CommonModule,
    EmpCommonModule,
    EmployeesRoutingModule,
  ]
})
export class EmployeesModule { }
