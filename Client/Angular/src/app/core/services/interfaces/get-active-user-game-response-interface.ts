import { SudokuCellInterface } from './sudoku-cell-interface';
import { GameLevelInterface } from './game-level-interface';

export interface GetActiveUserGameResponseInterface {

  createdDaateTime: string;
  time: number;
  wrong: number;
  hint: number;
  data?: SudokuCellInterface[][];
  level: GameLevelInterface;

}
