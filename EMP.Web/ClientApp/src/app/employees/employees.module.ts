import { NgModule } from '@angular/core';
import { EmpCommonModule } from '../shared/emp-common.module';
import { EmpStyleModule } from './../shared/emp-style.module';
import { EmployeesRoutingModule } from './employees-routing.module';

import { EmployeeListComponent } from './list/employee-list.component';
import { EmployeeDetailComponent } from './detail/employee-detail.component';
import { EmployeeEditComponent } from './edit/employee-edit.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@NgModule({
  declarations: [
    EmployeeListComponent,
    EmployeeDetailComponent,
    EmployeeEditComponent
  ],
  imports: [
    EmpCommonModule,
    EmployeesRoutingModule,
    EmpStyleModule,
    NgxDatatableModule,
  ]
})
export class EmployeesModule { }
