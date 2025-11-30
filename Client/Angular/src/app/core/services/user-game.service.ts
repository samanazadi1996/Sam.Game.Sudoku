// Auto-generated service for tag: UserGame
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import { GetActiveUserGameResponseBaseResultInterface } from './interfaces/get-active-user-game-response-base-result-interface';
import { GetUserGameStateResponseBaseResultInterface } from './interfaces/get-user-game-state-response-base-result-interface';
import { CreateGameCommandInterface } from './interfaces/create-game-command-interface';
import { BaseResultInterface } from './interfaces/base-result-interface';
import { WriteSudokuCellCommandInterface } from './interfaces/write-sudoku-cell-command-interface';
import { SudokuCellBaseResultInterface } from './interfaces/sudoku-cell-base-result-interface';
import { ClearColumnCommandInterface } from './interfaces/clear-column-command-interface';
import { WriteNoteCommandInterface } from './interfaces/write-note-command-interface';

@Injectable({ providedIn: 'root' })
export class UserGameService {
    constructor(private http: HttpClient) { }


    getApiUserGameGetActiveUserGame() {

        let params = new HttpParams();
        
        return this.http.get<GetActiveUserGameResponseBaseResultInterface>(`${environment.serverUrl}/api/UserGame/GetActiveUserGame`, { params });
    }

    getApiUserGameGetUserGameState() {

        let params = new HttpParams();
        
        return this.http.get<GetUserGameStateResponseBaseResultInterface>(`${environment.serverUrl}/api/UserGame/GetUserGameState`, { params });
    }

    postApiUserGameCreateGame(body : CreateGameCommandInterface) {
        return this.http.post<BaseResultInterface>(`${environment.serverUrl}/api/UserGame/CreateGame`, body);
    }

    postApiUserGameWriteSudokuCell(body : WriteSudokuCellCommandInterface) {
        return this.http.post<SudokuCellBaseResultInterface>(`${environment.serverUrl}/api/UserGame/WriteSudokuCell`, body);
    }

    postApiUserGameClearColumn(body : ClearColumnCommandInterface) {
        return this.http.post<SudokuCellBaseResultInterface>(`${environment.serverUrl}/api/UserGame/ClearColumn`, body);
    }

    postApiUserGameWriteNote(body : WriteNoteCommandInterface) {
        return this.http.post<SudokuCellBaseResultInterface>(`${environment.serverUrl}/api/UserGame/WriteNote`, body);
    }

    postApiUserGameCheckFinally() {
        return this.http.post<BaseResultInterface>(`${environment.serverUrl}/api/UserGame/CheckFinally`, { });
    }

}
