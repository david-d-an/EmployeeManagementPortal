import { NgModule } from '@angular/core';
// import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { LayoutComponent } from './layout/layout.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';

@NgModule({
  imports: [
    // CommonModule,
    NgbModule
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
