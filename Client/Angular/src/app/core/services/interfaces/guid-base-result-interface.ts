import { ErrorInterface } from './error-interface';

export interface GuidBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data: string;

}
