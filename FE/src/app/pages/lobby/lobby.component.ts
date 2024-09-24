import { NgFor, CommonModule } from "@angular/common";
import { ChangeDetectionStrategy, Component } from "@angular/core";
import { StartGameComponent } from "../start-game/start-game.component";

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

    constructor() {
        if (typeof localStorage !== 'undefined') {
            this.gameResponse = localStorage.getItem('GameResponse');
            this.players = this.gameResponse ? JSON.parse(this.gameResponse).players : [];
        } else {
            this.gameResponse = null;
            this.players = [];
        }
    }
}