import { GameLevelInterface } from './game-level-interface';

export interface GetUserGameStateActiveStateResponseInterface {

  level: GameLevelInterface;
  isActive: boolean;
  needPointLevelToUnlock: GameLevelInterface;
  needPointToUnlock?: number;

}
