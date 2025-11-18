import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './pages/login/login.component';
import { SharedModule } from '../../core/shared/shared.module';


@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    AccountRoutingModule,
    SharedModule
  ]
})
export class AccountModule { }
