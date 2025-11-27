// Auto-generated service for tag: Account
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import { AuthenticateCommandInterface } from './interfaces/authenticate-command-interface';
import { AuthenticationResponseBaseResultInterface } from './interfaces/authentication-response-base-result-interface';
import { BaseResultInterface } from './interfaces/base-result-interface';
import { ChangeUserNameCommandInterface } from './interfaces/change-user-name-command-interface';
import { ChangePasswordCommandInterface } from './interfaces/change-password-command-interface';
import { GetProfileResponseBaseResultInterface } from './interfaces/get-profile-response-base-result-interface';

@Injectable({ providedIn: 'root' })
export class AccountService {
    constructor(private http: HttpClient) { }


    postApiAccountAuthenticate(body : AuthenticateCommandInterface) {
        return this.http.post<AuthenticationResponseBaseResultInterface>(`${environment.serverUrl}/api/Account/Authenticate`, body);
    }

    postApiAccountStart() {
        return this.http.post<AuthenticationResponseBaseResultInterface>(`${environment.serverUrl}/api/Account/Start`, { });
    }

    postApiAccountLogOut() {
        return this.http.post<BaseResultInterface>(`${environment.serverUrl}/api/Account/LogOut`, { });
    }

    postApiAccountChangeUserName(body : ChangeUserNameCommandInterface) {
        return this.http.post<AuthenticationResponseBaseResultInterface>(`${environment.serverUrl}/api/Account/ChangeUserName`, body);
    }

    postApiAccountChangePassword(body : ChangePasswordCommandInterface) {
        return this.http.post<AuthenticationResponseBaseResultInterface>(`${environment.serverUrl}/api/Account/ChangePassword`, body);
    }

    getApiAccountGetProfile(userName? :string | null) {

        let params = new HttpParams();
        if (userName !== null && userName !== undefined)
            params = params.set('UserName', userName);

        return this.http.get<GetProfileResponseBaseResultInterface>(`${environment.serverUrl}/api/Account/GetProfile`, { params });
    }

}
