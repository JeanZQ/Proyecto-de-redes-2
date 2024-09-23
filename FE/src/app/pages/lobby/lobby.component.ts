import { NgFor, CommonModule } from "@angular/common";
import { ChangeDetectionStrategy, Component } from "@angular/core";
import {playersImg} from "../../../assets/players.json";
import { StartGameComponent } from "../start-game/start-game.component";
import { JoinGame } from "../../models/app.interface";

@Component({
    selector: 'lobby',
    standalone: true,
    imports: [
    CommonModule,
    NgFor,
    StartGameComponent
],
    templateUrl: './lobby.component.html',
    styleUrl: './lobby.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
})

export class LobbyComponent {
    readonly gameResponse: string | null;
    readonly players: any[];
    readonly playersImg: string[];
    readonly gameInfo: any;

    game:JoinGame ={
        id: '',
        player: '',
        owner: '',
        password: ''
    };

    constructor() {
        if (typeof localStorage !== 'undefined') {
            this.gameResponse = localStorage.getItem('GameResponse');
            this.gameInfo =  localStorage.getItem('PlayerInfo');

            this.game.owner = this.gameResponse ? JSON.parse(this.gameResponse).owner : '';
            this.game.player = this.gameInfo ? JSON.parse(this.gameInfo).player : '';
            this.game.id = this.gameResponse ? JSON.parse(this.gameResponse).id : '';
            this.game.password = this.gameInfo ? JSON.parse(this.gameInfo).password : '';
            this.players = this.gameResponse ? JSON.parse(this.gameResponse).players : [];
        } else {
            this.gameResponse = null;
            this.players = [];
        }
        this.playersImg = playersImg;
        console.log(this.game);
    }
}