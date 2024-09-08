import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { NewGame, ServerGameResponse,JoinGame } from "../models/app.interface";

@Injectable({
    providedIn: 'root'
    })
export class DataService{

    private urlAPI = 'https://contaminados.akamai.meseguercr.com/api/games';

    constructor(private http: HttpClient) {}

    getRooms():Observable<ServerGameResponse>{
        return this.http.get<ServerGameResponse>(this.urlAPI);
    }

    createRoom(payload:NewGame):Observable<ServerGameResponse>{
        return this.http.post<ServerGameResponse>(this.urlAPI, payload);
    }

    getGame(payload:JoinGame):Observable<ServerGameResponse>{
        return this.http.get<ServerGameResponse>(`${this.urlAPI}/${payload.id}`,
            {headers: {'password': payload.password,'player': payload.owner
        }});
    }

    joinGame(payload:JoinGame):Observable<ServerGameResponse>{
        return this.http.put<ServerGameResponse>(`${this.urlAPI}/${payload.id}`, {player: payload.player},
            {headers: {'password': payload.password,'player': payload.owner
        }});
    }


    //   public fetchJoinGame(owner: string, player: string, password: string, id: string) {
    //     const headers = new HttpHeaders({
    //       'accept': 'application/json',
    //       'password': password,
    //       'player': owner,  
    //       'Content-Type': 'application/json'
    //     });
      
    //     const body = {
    //       player: player 
    //     };
      
    //     this.http.put(`https://contaminados.akamai.meseguercr.com/api/games/${id}`, body, { headers })
    //       .subscribe(
    //         (response: any) => {
    //           this.dataGetGame = response;
    //           console.log(this.dataGetGame);
    //           //localStorage.setItem("getGameResponse", JSON.stringify(this.dataGetGame));
    //         },
    //         (error: any) => {
    //           console.log(error);
    //         }
    //       );
    //   } 

} 
