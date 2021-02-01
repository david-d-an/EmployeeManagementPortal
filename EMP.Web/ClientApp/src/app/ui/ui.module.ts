import { EmpCommonModule } from './../shared/emp-common.module';
import { EmpStyleModule } from './../shared/emp-style.module';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { LayoutComponent } from './layout/layout.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
// import { CommonModule } from '@angular/common';

@NgModule({
  imports: [
    EmpCommonModule,
    EmpStyleModule,
    RouterModule
  ],
  exports: [
    LayoutComponent
  ],
  declarations: [
    LayoutComponent,
    HeaderComponent,
    FooterComponent
  ]
})
export class UiModule { }
