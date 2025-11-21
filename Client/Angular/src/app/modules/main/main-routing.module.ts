import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { GameComponent } from './game/game.component';
import { CreateGameComponent } from './create-game/create-game.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      { path: '', redirectTo: 'create-game', pathMatch: 'full' },
      { path: '', component: MainComponent },
      { path: 'game', component: GameComponent },
      { path: 'create-game', component: CreateGameComponent },
    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
