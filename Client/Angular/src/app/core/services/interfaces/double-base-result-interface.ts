import { ErrorInterface } from './error-interface';

export interface DoubleBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data: number;

}
