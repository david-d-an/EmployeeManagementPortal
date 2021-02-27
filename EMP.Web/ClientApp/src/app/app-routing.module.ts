import { AuthRouteGuard } from './user/auth.routeguard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { SigninRedirectCallbackComponent } from './home/signin-redirect-callback.component';
import { SignoutRedirectCallbackComponent } from './home/signout-redirect-callback.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AboutComponent } from './about/about.component';
import { ContactComponent } from './contact/contact.component';

// import { DepartmentListComponent } from './departments/list/department-list.component';
// import { EmployeeListComponent } from './employees/list/employee-list.component';
// import { CommonModule } from '@angular/common';
// import { CounterComponent } from './counter/counter.component';
// import { FetchDataComponent } from './fetch-data/fetch-data.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  // { path: 'home', component: HomeComponent, canActivate: [AuthRouteGuard] },
  { path: 'signin-callback', component: SigninRedirectCallbackComponent },
  { path: 'signout-callback', component: SignoutRedirectCallbackComponent },
  { path: 'welcome', redirectTo: 'home', pathMatch: 'full' },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  // { path: 'employees', component: EmployeeListComponent },
  { path: 'employees',
    loadChildren: () =>
      import('./employees/employees.module').then(m => m.EmployeesModule),
    canActivate: [AuthRouteGuard],
    canLoad: [AuthRouteGuard],
    data: { preload: true, delay: 1000 }
  },
  // { path: 'departments', component: DepartmentListComponent },
  { path: 'departments',
    loadChildren: () =>
      import('./departments/departments.module').then(m => m.DepartmentsModule),
    canActivate: [AuthRouteGuard],
    canLoad: [AuthRouteGuard],
    data: { preload: true, delay: 1000 }
  },
  { path: 'contact', component: ContactComponent },
  { path: 'about', component: AboutComponent },
  // { path: 'counter', component: CounterComponent },
  // { path: 'fetch-data', component: FetchDataComponent },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [
    // CommonModule,
    RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })
  ],
  exports: [
    RouterModule
  ],
  declarations: []
})
export class AppRoutingModule {

}
