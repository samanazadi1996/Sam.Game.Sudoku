import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { MainComponent } from '../main/main.component';
import { GameComponent } from './game/game.component';
import { CreateGameComponent } from './create-game/create-game.component';
import { JalaliDatePipe } from "../../core/pipes/jalali-date.pipe";
import { GameLevelPipe } from '../../core/pipes/game-level';


@NgModule({
  declarations: [
    MainComponent,
    GameComponent,
    CreateGameComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    JalaliDatePipe,
    GameLevelPipe]
})
export class MainModule { }
