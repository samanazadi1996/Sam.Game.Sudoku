import { ErrorInterface } from './error-interface';
import { CheckResponseInterface } from './check-response-interface';

export interface CheckResponseBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data: CheckResponseInterface;

}
