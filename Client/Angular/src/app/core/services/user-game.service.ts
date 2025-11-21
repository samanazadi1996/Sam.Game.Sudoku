// Auto-generated service for tag: UserGame
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import { GetActiveUserGameResponseBaseResultInterface } from './interfaces/get-active-user-game-response-base-result-interface';
import { HasSavedGameResponseBaseResultInterface } from './interfaces/has-saved-game-response-base-result-interface';
import { CreateGameCommandInterface } from './interfaces/create-game-command-interface';
import { BaseResultInterface } from './interfaces/base-result-interface';
import { CheckCommandInterface } from './interfaces/check-command-interface';
import { SudokuCellBaseResultInterface } from './interfaces/sudoku-cell-base-result-interface';

@Injectable({ providedIn: 'root' })
export class UserGameService {
    constructor(private http: HttpClient) { }


    getApiUserGameGetActiveUserGame() {

        let params = new HttpParams();
        
        return this.http.get<GetActiveUserGameResponseBaseResultInterface>(`${environment.serverUrl}/api/UserGame/GetActiveUserGame`, { params });
    }

    getApiUserGameHasSavedGame() {

        let params = new HttpParams();
        
        return this.http.get<HasSavedGameResponseBaseResultInterface>(`${environment.serverUrl}/api/UserGame/HasSavedGame`, { params });
    }

    postApiUserGameCreateGame(body : CreateGameCommandInterface) {
        return this.http.post<BaseResultInterface>(`${environment.serverUrl}/api/UserGame/CreateGame`, body);
    }

    postApiUserGameCheck(body : CheckCommandInterface) {
        return this.http.post<SudokuCellBaseResultInterface>(`${environment.serverUrl}/api/UserGame/Check`, body);
    }

}
