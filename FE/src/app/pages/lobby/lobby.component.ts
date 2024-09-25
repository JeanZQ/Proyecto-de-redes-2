import { NgFor, CommonModule } from "@angular/common";
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy } from "@angular/core";
import { StartGameComponent } from "../start-game/start-game.component";
import { StartGame } from "../../models/app.interface";
import { DataService } from "../../services/data.service";
import { interval, Subscription } from 'rxjs';

@Component({
    selector: 'lobby',
    standalone: true,
    imports: [
        CommonModule,
        NgFor,
        StartGameComponent
    ],
    templateUrl: './lobby.component.html',
    styleUrls: ['./lobby.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})

export class LobbyComponent implements OnDestroy {
    readonly gameResponse: string | null;
    players: any[] = []; // Lista de jugadores
    readonly gameInfo: any;
    private subscription: Subscription | null = null;
    readonly gameName : string | null | undefined;

    readonly game: StartGame = {
        id: '',
        player: '',
        password: ''
    };

    constructor(
        private dataService: DataService,
        private cdr: ChangeDetectorRef
    ) {
        if (typeof localStorage !== 'undefined') {
            this.gameResponse = localStorage.getItem('GameResponse');
            this.gameInfo = localStorage.getItem('PlayerInfo');
            this.game.id = this.gameResponse ? JSON.parse(this.gameResponse).id : '';
            this.game.player = this.gameInfo ? JSON.parse(this.gameInfo).player : '';
            this.game.password = this.gameInfo ? JSON.parse(this.gameInfo).password : '';
            this.players = this.gameResponse ? JSON.parse(this.gameResponse).players : [];
            this.gameName = this.gameResponse ? JSON.parse(this.gameResponse).name : '';
            // Llama al servicio cada 5 segundos
            this.subscription = interval(5000).subscribe(() => {
                this.dataService.getGame(this.game).subscribe({
                    next: (response: any) => {
                        // Actualiza los jugadores sin recargar la página
                        this.updatePlayers(response);

                    },
                    error: (error: any) => {
                        console.log(error);
                    }
                });
            });
        } else {
            this.gameResponse = null;
            this.players = [];
        }
    }

    // Método para destruir la suscripción cuando el componente se destruya
    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }

    updatePlayers(response: any) {
        this.players = response.data.players; // Actualiza la lista de jugadores
        localStorage.setItem('GameResponse', JSON.stringify(response.data)); // Actualiza el localStorage
        console.log('Players updated:', this.players);
        this.cdr.detectChanges(); // Actualiza la vista
    }


}
