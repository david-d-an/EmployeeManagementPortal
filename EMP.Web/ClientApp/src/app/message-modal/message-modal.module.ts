import { EmpStyleModule } from './../shared/emp-style.module';
import { MessageModalComponent } from './message-modal.component';
import { NgModule } from '@angular/core';
import { EmpCommonModule } from '../shared/emp-common.module';

@NgModule({
  declarations: [
    MessageModalComponent
  ],
  imports: [
    EmpCommonModule,
    EmpStyleModule,
  ],
  exports: [
    MessageModalComponent
  ]
})
export class MessageModalModule { }
