import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { NewGame, ServerGameResponse, JoinGame, SearchGame } from "../models/app.interface";

@Injectable({
    providedIn: 'root'
})
export class DataService {

    private urlAPI = 'https://contaminados.akamai.meseguercr.com/api/games';

    constructor(private http: HttpClient) { }

    getRooms(): Observable<ServerGameResponse> {
        return this.http.get<ServerGameResponse>(this.urlAPI);
    }

    createRoom(payload: NewGame): Observable<ServerGameResponse> {
        console.log(payload);
        return this.http.post<ServerGameResponse>(this.urlAPI, payload);
    }

    getGame(payload: JoinGame): Observable<ServerGameResponse> {
        return this.http.get<ServerGameResponse>(`${this.urlAPI}/${payload.id}`,
            {
                headers: {
                    'password': payload.password, 'player': payload.owner
                }
            });
    }

    joinGame(payload: JoinGame): Observable<ServerGameResponse> {
        return this.http.put<ServerGameResponse>(`${this.urlAPI}/${payload.id}`, { player: payload.player },
            {
                headers: {
                    'password': payload.password, 'player': payload.owner
                }
            });
    }
  
    gamesearch(payload: SearchGame): Observable<ServerGameResponse> {
        return this.http.get<ServerGameResponse>(`${this.urlAPI}/${'?name=' + payload.name + '&status=' + payload.status + '&page=' + payload.page, '&limit=' + payload.limit}`);
    }
}