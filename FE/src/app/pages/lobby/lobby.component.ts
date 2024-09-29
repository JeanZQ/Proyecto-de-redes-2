import { NgFor, CommonModule } from "@angular/common";
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, Inject, OnDestroy } from "@angular/core";
import { StartGameComponent } from "../start-game/start-game.component";
import { RoundInfoRequest, RoundResponse, StartGame } from "../../models/app.interface";
import { DataService } from "../../services/data.service";
import { interval, Subscription } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { FormsModule, ReactiveFormsModule,FormBuilder } from "@angular/forms";
import { SelectRoundGroupComponent } from "../select-round-group/select-round-group.component";


@Component({
    selector: 'lobby',
    standalone: true,
    imports: [
        CommonModule,
        NgFor,
        StartGameComponent,
        MatButtonModule, 
        MatIconModule,
        MatCheckboxModule,
        ReactiveFormsModule,
        FormsModule,
        SelectRoundGroupComponent
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
    rounds: any[] = []; // lista de rounds
    hasPassword: boolean = false;
    enemies: string[] = [];
    private readonly _formBuiler = inject(FormBuilder);
    private roundGroup:string[] = [];
    public roundLeader: string | '' | undefined;
    public groupDefined: boolean = false;
    public leader: boolean = false;
    public roundPayload: RoundInfoRequest = {
        gameId: '',
        roundId: '',  
        player: ''
    }; 
        
    readonly game: StartGame = {
        id: '',
        player: '',
        password: ''
    };

    readonly selectedPlayers = this._formBuiler.group({
        player:''
    });

    roundResponse: RoundResponse = {
        status: 0,
        msg: '',
        data: {
            roundId: '',
            leader: '',
            status: '',
            result: '',
            phase: '',
            group: [],
            votes: []
        }
    }
    

    constructor(
        private dataService: DataService,
        private cdr: ChangeDetectorRef
    ) 
    
    
    
    { 
        if (typeof localStorage !== 'undefined') {
            this.gameResponse = localStorage.getItem('GameResponse');
            this.gameInfo = localStorage.getItem('PlayerInfo');
            this.game.id = this.gameResponse ? JSON.parse(this.gameResponse).id : '';
            this.game.player = this.gameInfo ? JSON.parse(this.gameInfo).player : '';
            this.game.password = this.gameInfo ? JSON.parse(this.gameInfo).password : '';
            this.players = this.gameResponse ? JSON.parse(this.gameResponse).players : [];
            this.gameName = this.gameResponse ? JSON.parse(this.gameResponse).name : '';
            let roundId = this.gameResponse ? JSON.parse(this.gameResponse).currentRound : '';

            // payload para obtener una ronda
            this.roundPayload = {
                gameId: this.game.id,
                roundId: roundId,  
                player: this.game.player
            };
                    


            // verifica si tiene password
            if(this.game.password && this.game.password.trim() !== '') {
                this.hasPassword = true;
              }

            // Llama al servicio cada 5 segundos
            this.subscription = interval(5000).subscribe(() => {

                this.getRound();
                console.log('ENEMIGOS:' + this.enemies);


                this.dataService.getGame(this.game).subscribe({
                    next: (response: any) => {
                        // Actualiza los jugadores sin recargar la página
                        this.updatePlayers(response);
                        if (this.gameResponse) {
                            this.roundPayload = {
                                gameId: this.game.id,
                                roundId: JSON.parse(this.gameResponse).currentRound,  
                                player: this.game.player
                            };
                        }

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
        this.enemies = response.data.enemies; // Suponiendo que los enemigos están en response.data.enemies
   
        if(!this.leader && this.game.player == response.data.owner) {
            console.log('El jugador es el dueño de la sala');
            this.leader = true;
        }

        if (this.isCurrentPlayerEnemy()) {
            console.log(`${this.game.player} es un enemigo.`);
        } else {
            console.log(`${this.game.player} NO es un enemigo.`);
        }
    
    }    


    getRound() {

        console.log('PAYLOAD: ' + this.gameResponse);

        //si tiene password, añadirlo al payload
        if(this.hasPassword) {
            this.roundPayload.password = this.game.password;
        }

        console.log('PAYLOAD: ');
        console.log(this.roundPayload);
        // if(this.roundPayload.player == undefined) {
        //    this.roundPayload.player = this.gameResponse ? JSON.parse(this.gameResponse).player : '';
        //    this.game.player = this.gameResponse ? JSON.parse(this.gameResponse).player : '';
        // }



        this.dataService.getRound(this.roundPayload).subscribe({
            
            next: (response: any) => {
                this.roundResponse = response; // Actualiza la ronda
                localStorage.setItem('RoundResponse', JSON.stringify(response.data)); // Guarda la ronda en localStorage
                this.roundLeader = response.data.leader; // Actualiza el líder de la ronda
                console.log('Round obtained:', this.roundResponse);
                this.cdr.detectChanges(); // Actualiza la vista

                

            },
            error: (error: any) => {
                console.error('Error al obtener la ronda:', error);
            }
        });

      
    }

    // devuelve si el player actual es enemigo
    isCurrentPlayerEnemy(): boolean {
        return this.enemies.includes(this.game.player);
    }


    onPlayerSelect(player: string, event: boolean) {
        console.log('Player selected:', player, event);

        if (event) {
            this.roundGroup.push(player);
        } else {
            this.roundGroup = this.roundGroup.filter(p => p !== player);
        }
        
        
    }


    selectGroup() {
        console.log('Round group:', this.roundGroup);
    }

}
