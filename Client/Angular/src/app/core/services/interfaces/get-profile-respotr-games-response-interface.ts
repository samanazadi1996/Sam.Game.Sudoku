import { GameLevelInterface } from './game-level-interface';

export interface GetProfileRespotrGamesResponseInterface {

  gameLevel: GameLevelInterface;
  endedSucceess: number;
  endedFailed: number;
  inactive: number;

}
