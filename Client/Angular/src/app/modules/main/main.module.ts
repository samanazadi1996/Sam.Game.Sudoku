import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { MainComponent } from '../main/main.component';
import { GameComponent } from './pages/game/game.component';
import { CreateGameComponent } from './pages/create-game/create-game.component';
import { JalaliDatePipe } from "../../core/pipes/jalali-date.pipe";
import { GameLevelPipe } from '../../core/pipes/game-level';
import { ProfileComponent } from './pages/profile/profile.component';
import { SettingsComponent } from './pages/settings/settings.component';


@NgModule({
  declarations: [
    MainComponent,
    GameComponent,
    CreateGameComponent,
    ProfileComponent,
    SettingsComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    JalaliDatePipe,
    GameLevelPipe]
})
export class MainModule { }
