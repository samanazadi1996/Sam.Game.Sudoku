import { ErrorInterface } from './error-interface';

export interface StringBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data?: string;

}
