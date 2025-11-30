import { ErrorInterface } from './error-interface';
import { GetUserGameStateResponseInterface } from './get-user-game-state-response-interface';

export interface GetUserGameStateResponseBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data: GetUserGameStateResponseInterface;

}
