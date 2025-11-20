import { ErrorInterface } from './error-interface';
import { SudokuCellInterface } from './sudoku-cell-interface';

export interface SudokuCellBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data: SudokuCellInterface;

}
