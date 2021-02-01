import { NgModule } from '@angular/core';
// import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeListComponent } from './list/employee-list.component';
import { EmployeeDetailComponent } from './detail/employee-detail.component';
import { EmployeeEditComponent } from './edit/employee-edit.component';


const routes: Routes = [
  {
    path: '',
    component: EmployeeListComponent,
    data: { pageTitle: 'Employees List' },
    // canActivate: [AuthGuard]
  },
  {
    path: ':id',
    component: EmployeeDetailComponent,
    // resolve: { productResolved: ProductResolver },
    // canActivate: [AuthGuard]
  },
  {
    path: ':id/edit',
    component: EmployeeEditComponent,
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
    // CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class EmployeesRoutingModule { }
