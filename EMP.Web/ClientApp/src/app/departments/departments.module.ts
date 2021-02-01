import { EmpCommonModule } from '../shared/emp-common.module';
import { NgModule } from '@angular/core';
// import { CommonModule } from '@angular/common';
import { DepartmentsRoutingModule } from './departments-routing.module';
import { DepartmentListComponent } from './list/department-list.component';
import { DepartmentDetailComponent } from './detail/department-detail.component';
import { DepartmentEditComponent } from './edit/department-edit.component';

@NgModule({
  declarations: [
    DepartmentListComponent,
    DepartmentDetailComponent,
    DepartmentEditComponent
  ],
  imports: [
    // CommonModule,
    EmpCommonModule,
    DepartmentsRoutingModule,
  ]
})
export class DepartmentsModule { }
