import { ErrorInterface } from './error-interface';
import { GetTopPlayersPagedListResponseInterface } from './get-top-players-paged-list-response-interface';

export interface GetTopPlayersPagedListResponsePagedResponseInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data?: GetTopPlayersPagedListResponseInterface[];
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  totalItems: number;

}
