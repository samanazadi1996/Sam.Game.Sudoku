import { Component, OnInit } from '@angular/core';
import { UserGameService } from '../../../../core/services/user-game.service';
import { Router } from '@angular/router';
import { HasSavedGameResponseBaseResultInterface } from '../../../../core/services/interfaces/has-saved-game-response-base-result-interface';
import { GeneralService } from '../../../../core/services/general.service';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrl: './create-game.component.scss'
})
export class CreateGameComponent implements OnInit {
  savedGame?: HasSavedGameResponseBaseResultInterface;

  constructor(private userGameService: UserGameService, private router: Router, private generalService: GeneralService) {
  }
  ngOnInit(): void {
    this.generalService.setButtonsState({
      homeDisplay: false,
      profileDisplay: true,
      settingsDisplay: true,
      title: "Sudoku"
    })
    this.userGameService.getApiUserGameHasSavedGame().subscribe(response => {
      this.savedGame = response
    })


  }

  createGame(level: number) {
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
