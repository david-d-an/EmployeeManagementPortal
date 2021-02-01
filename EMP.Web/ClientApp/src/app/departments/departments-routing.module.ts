import { NgModule } from '@angular/core';
import { EmpCommonModule } from './../shared/emp-common.module';
import { Routes, RouterModule } from '@angular/router';

import { DepartmentListComponent } from './list/department-list.component';
import { DepartmentDetailComponent } from './detail/department-detail.component';
import { DepartmentEditComponent } from './edit/department-edit.component';


const routes: Routes = [
  {
    path: '',
    component: DepartmentListComponent,
    data: { pageTitle: 'Department List' },
    // canActivate: [AuthGuard]
  },
  {
    path: ':id',
    component: DepartmentDetailComponent,
    // resolve: { productResolved: ProductResolver },
    // canActivate: [AuthGuard]
  },
  {
    path: ':id/edit',
    component: DepartmentEditComponent,
    // resolve: { productResolved: ProductResolver },
    // canActivate: [AuthGuard],
    // canDeactivate: [ProductEditGuard],
    // children: [
    //   { path: '', redirectTo: 'info', pathMatch: 'full' },
    //   { path: 'info', component: ProductEditInfoComponent },
    //   { path: 'tags', component: ProductEditTagsComponent },
    // ]
  },
];

@NgModule({
  declarations: [],
  imports: [
    EmpCommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class DepartmentsRoutingModule { }
