// Auto-generated service for tag: User
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import { UserDtoPagedResponseInterface } from './interfaces/user-dto-paged-response-interface';
import { CreateUserCommandInterface } from './interfaces/create-user-command-interface';
import { GuidBaseResultInterface } from './interfaces/guid-base-result-interface';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }


    getApiUserGetUsersPagedList(userName? :string | null,firstName? :string | null,lastName? :string | null,isActive? :boolean | null,pageNumber? :number | null,pageSize? :number | null) {

        let params = new HttpParams();
        if (userName !== null && userName !== undefined)
            params = params.set('UserName', userName);

        if (firstName !== null && firstName !== undefined)
            params = params.set('FirstName', firstName);

        if (lastName !== null && lastName !== undefined)
            params = params.set('LastName', lastName);

        if (isActive !== null && isActive !== undefined)
            params = params.set('IsActive', isActive);

        if (pageNumber !== null && pageNumber !== undefined)
            params = params.set('PageNumber', pageNumber);

        if (pageSize !== null && pageSize !== undefined)
            params = params.set('PageSize', pageSize);

        return this.http.get<UserDtoPagedResponseInterface>(`${environment.serverUrl}/api/User/GetUsersPagedList`, { params });
    }

    postApiUserCreateUser(body : CreateUserCommandInterface) {
        return this.http.post<GuidBaseResultInterface>(`${environment.serverUrl}/api/User/CreateUser`, body);
    }

}
