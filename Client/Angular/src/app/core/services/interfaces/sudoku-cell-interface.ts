import { SudokuCellStatusInterface } from './sudoku-cell-status-interface';

export interface SudokuCellInterface {

  number?: number;
  status: SudokuCellStatusInterface;
  note?: number[];

}
