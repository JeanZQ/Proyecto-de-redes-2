import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable, Input } from "@angular/core";
import { Observable } from "rxjs";

import { NewGame, ServerGameResponse, JoinGame, SearchGame, StartGame, DEFAULT_PASSWORD, RoundInfoData, RoundResponse, AllRoundsInfoRequest, RoundInfoRequest, ProposeRound, VoteGroup } from "../models/app.interface";



@Injectable({
    providedIn: 'root'
})
export class DataService {
    private urlAPI = 'https://localhost:5237/api/games';
    constructor(private http: HttpClient) { }

    getRooms(page: number, limit: number): Observable<ServerGameResponse> {
        return this.http.get<ServerGameResponse>(`${this.urlAPI}?page=${page}&limit=${limit}`);
    }

    createRoom(payload: NewGame): Observable<ServerGameResponse> {
        return this.http.post<ServerGameResponse>(this.urlAPI, payload);
    }

    getGame(payload: StartGame): Observable<ServerGameResponse> {
        const headers = new HttpHeaders({
            'player': payload.player,
            ...(payload.password && { 'password': payload.password })
        });

        return this.http.get<ServerGameResponse>(`${this.urlAPI}/${payload.id}`, { headers });
    }

    joinGame(payload: JoinGame): Observable<ServerGameResponse> {

        const headers = new HttpHeaders({
            'password': payload.password ? payload.password : DEFAULT_PASSWORD,
            'player': payload.owner,
            'Content-Type': 'application/json'
        });

        return this.http.put<ServerGameResponse>(
            `${this.urlAPI}/${payload.id}/`,
            { player: payload.player },
            { headers: headers }
        );
    }

    gamesearch(payload: SearchGame): Observable<ServerGameResponse> {
        return this.http.get<ServerGameResponse>(`${this.urlAPI}?name=${payload.name}&status=${payload.status}&page=${payload.page}&limit=${payload.limit}`);
    }

    startGame(payload: StartGame): Observable<ServerGameResponse> {
        return this.http.head<ServerGameResponse>(`${this.urlAPI}/${payload.id}/start`,
            {
                headers: {
                    'player': payload.player
                }
            }
        );
    }

    getRound(payload: RoundInfoRequest): Observable<RoundResponse> {
        return this.http.get<RoundResponse>(`${this.urlAPI}/${payload.gameId}/rounds/${payload.roundId}`,
            {
                headers: {
                    'player': payload.player,
                    ...(payload.password && { 'password': payload.password })
                }
            }
        );
    }

    // retorna todos los rounds de un juego
    getAllRounds(payload: AllRoundsInfoRequest): Observable<RoundResponse> {
        return this.http.get<RoundResponse>(`${this.urlAPI}/${payload.gameId}/rounds`,

            {
                headers: {
                    'player': payload.player,
                    ...(payload.password && { 'password': payload.password })
                }
            }

        );
    }

    // Vota por el grupo
    postVoteGroup(payload: VoteGroup) {
        return this.http.post<ServerGameResponse>(`${this.urlAPI}/${payload.gameId}/rounds/${payload.roundId}`,
            {
                vote: payload.vote
            },
            {
                headers: {
                    'player': payload.player,
                    ...(payload.password && { 'password': payload.password })
                }
            }
        );
    }

    proposeGroup(payload: ProposeRound) {
        return this.http.patch<RoundResponse>(`${this.urlAPI}/${payload.gameId}/rounds/${payload.roundId}`,
            {
                group: payload.group
            },
            {
                headers: {
                    'player': payload.player,
                    ...(payload.password && { 'password': payload.password })
                }
            }
        );
    }

    votePlayer(payload: VoteGroup) {
        // console.log('votePlayer: ',payload);
        return this.http.put<ServerGameResponse>(`${this.urlAPI}/${payload.gameId}/rounds/${payload.roundId}`,
            {
                action: payload.vote
            },
            {
                headers: {
                    'player': payload.player,
                    ...(payload.password && { 'password': payload.password })
                }
            }
        );
    }
}
