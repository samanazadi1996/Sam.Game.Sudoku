import { ErrorInterface } from './error-interface';
import { GetProfileResponseInterface } from './get-profile-response-interface';

export interface GetProfileResponseBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data: GetProfileResponseInterface;

}
