import { NgModule } from '@angular/core';
import { EmpCommonModule } from '../shared/emp-common.module';
import { EmpStyleModule } from '../shared/emp-style.module';
import { DepartmentsRoutingModule } from './departments-routing.module';

import { DepartmentListComponent } from './list/department-list.component';
import { DepartmentDetailComponent } from './detail/department-detail.component';
import { DepartmentEditComponent } from './edit/department-edit.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@NgModule({
  declarations: [
    DepartmentListComponent,
    DepartmentDetailComponent,
    DepartmentEditComponent
  ],
  imports: [
    EmpCommonModule,
    DepartmentsRoutingModule,
    EmpStyleModule,
    NgxDatatableModule,
  ]
})
export class DepartmentsModule { }
