import { NgFor, CommonModule } from "@angular/common";
import { ChangeDetectionStrategy, Component } from "@angular/core";
import {playersImg} from "../../../assets/players.json";

@Component({
    selector: 'lobby',
    standalone: true,
    imports: [
        CommonModule,
        NgFor
    ],
    templateUrl: './lobby.component.html',
    styleUrl: './lobby.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
})

export class LobbyComponent {
    readonly gameResponse: string | null;
    readonly players: any[];
    readonly playersImg: string[];

    constructor() {
        if (typeof localStorage !== 'undefined') {
            this.gameResponse = localStorage.getItem('GameResponse');
            this.players = this.gameResponse ? JSON.parse(this.gameResponse).players : [];
        } else {
            this.gameResponse = null;
            this.players = [];
        }

        this.playersImg = playersImg;
    }
}