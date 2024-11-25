import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable, Input } from "@angular/core";
import { Observable } from "rxjs";


import { NewGame, ServerGameResponse, JoinGame, SearchGame, StartGame, DEFAULT_PASSWORD, RoundInfoData, RoundResponse, AllRoundsInfoRequest, RoundInfoRequest, ProposeRound, VoteGroup, LinkBE } from "../models/app.interface";
import { Console } from "console";
import { MatSnackBar } from "@angular/material/snack-bar";



@Injectable({
    providedIn: 'root'
})
export class DataService {
    // https://contaminados.akamai.meseguercr.com/api/games
    // https://localhost:7047/api/games

    public linkBE : LinkBE = {
        url: ''
    };

    link : string = '';
    

    constructor(
        private http: HttpClient,
        private _snackBar: MatSnackBar,
    ) {
        if (typeof localStorage !== 'undefined') {
            const storedLinkBE = localStorage.getItem('BE');
            if (storedLinkBE) {
                this.linkBE = JSON.parse(storedLinkBE);
                console.log('LinkBE: ', storedLinkBE);            
          }else
          console.log('LinkBE: ', 'No hay LinkBE');
        }
        this.link = this.linkBE.url ? this.linkBE.url : 'https://contaminados.akamai.meseguercr.com';    
    }

    setLinkBE(linkBE: string) {
        console.log('Cambiando LinkBE: ', linkBE);
        this.linkBE.url = linkBE;
        this.link = this.linkBE.url;
        localStorage.setItem('BE', JSON.stringify(this.linkBE));
        this._snackBar.open('BackEnd Cambiado', 'Ok', {
            duration: 5000,
        });
    }

    getRooms(page: number, limit: number): Observable<ServerGameResponse> {
        return this.http.get<ServerGameResponse>(`${this.link+'/api/games'}?page=${page}&limit=${limit}`);
    }

    createRoom(payload: NewGame): Observable<ServerGameResponse> {
        return this.http.post<ServerGameResponse>(`${this.link+ '/api/games'}`, payload);
    }

    getGame(payload: StartGame): Observable<ServerGameResponse> {
        const headers = new HttpHeaders({
            'player': payload.player,
            ...(payload.password && { 'password': payload.password })
        });

        return this.http.get<ServerGameResponse>(`${this.link+'/api/games'}/${payload.id}`, { headers });
    }

    joinGame(payload: JoinGame): Observable<ServerGameResponse> {

        const headers = new HttpHeaders({
            'player': payload.owner,
            ...(payload.password && { 'password': payload.password })
        });

        return this.http.put<ServerGameResponse>(
            `${this.link+'/api/games'}/${payload.id}/`,
            { player: payload.player },
            { headers: headers }
        );
    }

    gamesearch(payload: SearchGame): Observable<ServerGameResponse> {
        console.log(this.link);
        return this.http.get<ServerGameResponse>(`${this.link+'/api/games'}?name=${payload.name}&status=${payload.status}&page=${payload.page}&limit=${payload.limit}`);
    }

    startGame(payload: StartGame): Observable<ServerGameResponse> {
        return this.http.head<ServerGameResponse>(`${this.link+'/api/games'}/${payload.id}/start`,
            {
                headers: {
                    'player': payload.player,
                    ...(payload.password && { 'password': payload.password })
                }
            }
        );
    }

    getRound(payload: RoundInfoRequest): Observable<RoundResponse> {
        return this.http.get<RoundResponse>(`${this.link+'/api/games'}/${payload.gameId}/rounds/${payload.roundId}`,
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
        return this.http.get<RoundResponse>(`${this.link+'/api/games'}/${payload.gameId}/rounds`,

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
        return this.http.post<ServerGameResponse>(`${this.link+'/api/games'}/${payload.gameId}/rounds/${payload.roundId}`,
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
        return this.http.patch<RoundResponse>(`${this.link+'/api/games'}/${payload.gameId}/rounds/${payload.roundId}`,
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
        return this.http.put<ServerGameResponse>(`${this.link+'/api/games'}/${payload.gameId}/rounds/${payload.roundId}`,
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
