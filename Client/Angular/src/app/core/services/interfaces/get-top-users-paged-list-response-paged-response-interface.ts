import { ErrorInterface } from './error-interface';
import { GetTopUsersPagedListResponseInterface } from './get-top-users-paged-list-response-interface';

export interface GetTopUsersPagedListResponsePagedResponseInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data?: GetTopUsersPagedListResponseInterface[];
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  totalItems: number;

}
