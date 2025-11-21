import { ErrorInterface } from './error-interface';
import { HasSavedGameResponseInterface } from './has-saved-game-response-interface';

export interface HasSavedGameResponseBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data: HasSavedGameResponseInterface;

}
