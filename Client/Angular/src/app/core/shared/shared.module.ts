import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ControlValidationComponent } from './components/control-validation/control-validation.component';
import { InputBoxComponent } from './components/input-box/input-box.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PaginationComponent } from './components/pagination/pagination.component';
import { JalaliDatePipe } from '../pipes/jalali-date.pipe';
import { LoadingComponent } from './components/loading/loading.component';
import { GameLevelPipe } from '../pipes/game-level';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    PaginationComponent,
    InputBoxComponent,
    ControlValidationComponent,
    LoadingComponent,
    JalaliDatePipe,
    GameLevelPipe
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    PaginationComponent,
    InputBoxComponent,
    ControlValidationComponent,
    LoadingComponent,
    JalaliDatePipe

  ]
})
export class SharedModule {}
