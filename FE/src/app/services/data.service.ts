import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { NewGame, ServerGameResponse } from "../models/app.interface";

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

} 
