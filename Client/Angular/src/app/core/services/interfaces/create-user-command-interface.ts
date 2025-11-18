

export interface CreateUserCommandInterface {

  userName?: string;
  firstName?: string;
  lastName?: string;
  password?: string;
  phoneNumber?: string;
  isActive: boolean;
  roles?: string[];

}
