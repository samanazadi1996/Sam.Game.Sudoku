import { Component, OnInit } from '@angular/core';
import { UserGameService } from '../../core/services/user-game.service';
import { GetActiveUserGameResponseInterface } from '../../core/services/interfaces/get-active-user-game-response-interface';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent implements OnInit {
  [x: string]: any;


  data?: GetActiveUserGameResponseInterface;
  selectedCell: { row: number, col: number } | null = null;
  constructor(private userGameService: UserGameService) {

  }
  ngOnInit(): void {
    this.userGameService.getApiUserGameGetActiveUserGame().subscribe(response => {
      this.data = response.data
    })
  }
  selectCell(i: number, j: number) {
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

    this.userGameService.postApiUserGameCheck({ col: col, number: num, row: row }).subscribe(response => {
      colomn.number = num
      colomn.status = response.data!.status
      this.checkFinal();
    })

    return;
  }
  checkFinal() {
    var ee = 0;
    this.data?.data!.forEach(element => {
      element.forEach(element => {
        if (element.status==0 || element.status==3)
          ee++
      });
    });

    if(ee==81)
      alert("You Won")
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
}
