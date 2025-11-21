import { GameLevelInterface } from './game-level-interface';

export interface HasSavedGameResponseInterface {

  createdDaateTime: string;
  time: number;
  level: GameLevelInterface;

}
