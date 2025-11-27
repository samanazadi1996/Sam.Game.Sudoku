

export interface CreateUserCommandInterface {

  userName?: string;
  nickName?: string;
  password?: string;
  phoneNumber?: string;
  isActive: boolean;
  roles?: string[];

}
