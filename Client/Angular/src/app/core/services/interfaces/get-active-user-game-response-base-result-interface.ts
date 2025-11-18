import { ErrorInterface } from './error-interface';
import { GetActiveUserGameResponseInterface } from './get-active-user-game-response-interface';

export interface GetActiveUserGameResponseBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data: GetActiveUserGameResponseInterface;

}
