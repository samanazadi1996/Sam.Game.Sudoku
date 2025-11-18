import { ErrorInterface } from './error-interface';
import { UserDtoInterface } from './user-dto-interface';

export interface UserDtoPagedResponseInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data?: UserDtoInterface[];
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  totalItems: number;

}
