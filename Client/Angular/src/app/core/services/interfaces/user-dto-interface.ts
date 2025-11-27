import { RoleDtoInterface } from './role-dto-interface';

export interface UserDtoInterface {

  id: string;
  userName?: string;
  nickName?: string;
  created: string;
  phoneNumber?: string;
  isActive: boolean;
  roles?: RoleDtoInterface[];

}
