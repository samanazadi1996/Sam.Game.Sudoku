import { Component, OnInit } from '@angular/core';
import { UserGameService } from '../../../../core/services/user-game.service';
import { Router } from '@angular/router';
import { GeneralService } from '../../../../core/services/general.service';
import { GetUserGameStateResponseInterface } from '../../../../core/services/interfaces/get-user-game-state-response-interface';
import { GameLevelInterface } from '../../../../core/services/interfaces/game-level-interface';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrl: './create-game.component.scss'
})
export class CreateGameComponent implements OnInit {
  data?: GetUserGameStateResponseInterface;

  constructor(private userGameService: UserGameService, private router: Router, private generalService: GeneralService) {
  }
  ngOnInit(): void {
    this.generalService.setButtonsState({
      homeDisplay: false,
      profileDisplay: true,
      settingsDisplay: true,
      title: "Sudoku"
    })
    this.userGameService.getApiUserGameGetUserGameState().subscribe(response => {
      this.data = response.data
    })


  }

  createGame(level: GameLevelInterface) {
    this.userGameService.postApiUserGameCreateGame({ gameLevel: level }).subscribe(response => {
      this.continueGame();
    })
  }

  continueGame() {
    this.router.navigate(['main', 'game'])
  }

  gotoTopUsers() {
    this.router.navigate(['main', 'top-users'])
  }

}
