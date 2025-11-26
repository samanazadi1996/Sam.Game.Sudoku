import { Component, OnInit } from '@angular/core';
import { GetActiveUserGameResponseInterface } from '../../../../core/services/interfaces/get-active-user-game-response-interface';
import { UserGameService } from '../../../../core/services/user-game.service';
import { Router } from '@angular/router';
import { GeneralService } from '../../../../core/services/general.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrl: './game.component.scss'
})
export class GameComponent implements OnInit {
  [x: string]: any;


  data?: GetActiveUserGameResponseInterface;
  selectedCell: { row: number, col: number } | null = null;
  constructor(private userGameService: UserGameService, private router: Router, private generalService: GeneralService) {
  }

  ngOnInit(): void {
    this.generalService.setButtonsState({
      backDisplay: true,
      profileDisplay: true,
      settingsDisplay: true,
      title: "Sudoku"
    })

    this.userGameService.getApiUserGameGetActiveUserGame().subscribe(response => {
      if (!response.success)
        this.router.navigate(["main", 'create-game'])
      else
        this.data = response.data
    })
  }
  selectCell(i: number, j: number) {
    if (this.selectedCell && this.selectedCell.col == j && this.selectedCell.row == i)
      this.selectedCell = null;
    else
      this.selectedCell = { row: i, col: j };
  }

  isHighlighted(i: number, j: number): boolean {
    if (!this.selectedCell) return false;

    const { row, col } = this.selectedCell;

    if (this.data)
      false

    if ((this.data?.data != null) && (this.data.data[row][col].number == null))
      return false;

    if (this.data!.data![row][col].number === this.data!.data![i][j].number) return true;

    const sameRow = row === i;
    const sameCol = col === j;

    // بلاک ۳×۳
    const sameBlock =
      Math.floor(row / 3) === Math.floor(i / 3) &&
      Math.floor(col / 3) === Math.floor(j / 3);

    return sameRow || sameCol || sameBlock;
  }

  write(num: number) {
    if (!this.selectedCell) return false;
    const { row, col } = this.selectedCell;

    var colomn = this.data!.data![row][col];
    if (colomn.status == 0) return;

    if (this.writeMode) {
      this.userGameService.postApiUserGameWriteSudokuCell({ col: col, number: num, row: row }).subscribe(response => {
        if (this.generalService.isSuccess(response)) {
          colomn.number = response.data!.number
          colomn.status = response.data!.status
          colomn.note = response.data!.note
          this.checkFinal();
        }
      })
    } else {
      this.userGameService.postApiUserGameWriteNote({ col: col, number: num, row: row }).subscribe(response => {
        if (this.generalService.isSuccess(response)) {
          colomn.number = response.data!.number
          colomn.status = response.data!.status
          colomn.note = response.data!.note
        }
      })
    }

    return;
  }
  disabledClearButton() {
    if (this.selectedCell == null) return true;
    const { row, col } = this.selectedCell;

    var colomn = this.data!.data![row][col];

    if (colomn.status == 0) return true;
    if (colomn.number == null && !colomn.note) return true;

    return false;
  }

  checkFinal() {
    var ee = 0;
    this.data?.data!.forEach(element => {
      element.forEach(element => {
        if (element.status == 0 || element.status == 3)
          ee++
      });
    });

    if (ee == 81) {
      this.userGameService.postApiUserGameCheckFinally().subscribe(response => {
        if (this.generalService.isSuccess(response)) {
          alert("You Won")
          this.router.navigate(['main', 'create-game'])
        }
      })
    }
  }
  getActionButtonStatus(num: number) {
    var ee = 0;
    this.data?.data!.forEach(element => {
      element.forEach(element => {
        if (element.number == num)
          ee++
      });
    });
    return !(ee >= 9);
  }
  clearCol() {
    if (!this.selectedCell) return;
    const { row, col } = this.selectedCell;

    this.userGameService.postApiUserGameClearColumn({ row: row, col: col })
      .subscribe(response => {
        if (this.generalService.isSuccess(response)) {

          var colomn = this.data!.data![row][col];
          colomn.number = response.data.number
          colomn.status = response.data.status
          colomn.note = response.data!.note
        }

      })
  }
  writeMode = true;
  setWriteMode(wm: boolean) {
    this.writeMode = wm;
  }
  getNote(notes: number[], r: number) {
    if (notes.includes(r))
      return r
    return '';
  }
}
