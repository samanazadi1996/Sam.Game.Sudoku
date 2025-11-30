import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { GameComponent } from './pages/game/game.component';
import { CreateGameComponent } from './pages/create-game/create-game.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { SettingsComponent } from './pages/settings/settings.component';
import { TopPlayersComponent } from './pages/top-players/top-players.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      { path: '', redirectTo: 'create-game', pathMatch: 'full' },
      { path: '', component: MainComponent },
      { path: 'game', component: GameComponent },
      { path: 'create-game', component: CreateGameComponent },
      { path: 'profile/:userName', component: ProfileComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'settings', component: SettingsComponent },
      { path: 'top-users', component: TopPlayersComponent },
    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
