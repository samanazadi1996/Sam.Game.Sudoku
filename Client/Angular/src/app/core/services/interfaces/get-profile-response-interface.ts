import { GetProfileRespotrGamesResponseInterface } from './get-profile-respotr-games-response-interface';

export interface GetProfileResponseInterface {

  self: boolean;
  nickName?: string;
  age: number;
  level: number;
  userName?: string;
  profileImage?: string;
  reportGames?: GetProfileRespotrGamesResponseInterface[];

}
