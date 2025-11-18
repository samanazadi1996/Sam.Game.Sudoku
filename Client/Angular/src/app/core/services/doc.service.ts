// Auto-generated service for tag: Doc
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import { StringBaseResultInterface } from './interfaces/string-base-result-interface';

@Injectable({ providedIn: 'root' })
export class DocService {
    constructor(private http: HttpClient) { }


    getApiDocGetErrorCodes() {

        let params = new HttpParams();
        
        return this.http.get(`${environment.serverUrl}/api/Doc/GetErrorCodes`, { params });
    }

    getApiDocGetVersion() {

        let params = new HttpParams();
        
        return this.http.get<StringBaseResultInterface>(`${environment.serverUrl}/api/Doc/GetVersion`, { params });
    }

}
