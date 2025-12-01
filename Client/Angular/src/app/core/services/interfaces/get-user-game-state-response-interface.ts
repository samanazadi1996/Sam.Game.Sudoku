import { GetUserGameStateActiveStateResponseInterface } from './get-user-game-state-active-state-response-interface';
import { GetUserGameStateSavedGameResponseInterface } from './get-user-game-state-saved-game-response-interface';

export interface GetUserGameStateResponseInterface {

  activeStates?: GetUserGameStateActiveStateResponseInterface[];
  savedGame: GetUserGameStateSavedGameResponseInterface;

}
