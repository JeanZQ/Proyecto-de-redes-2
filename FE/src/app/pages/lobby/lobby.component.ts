import { NgFor, CommonModule } from "@angular/common";
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy } from "@angular/core";
import { StartGameComponent } from "../start-game/start-game.component";
import { RoundInfoRequest, RoundResponse, StartGame } from "../../models/app.interface";
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
    rounds: any[] = []; // lista de rounds
    hasPassword: boolean = false;

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
                player: 'Churry'
            };
                    


            // verifica si tiene password
            if(this.game.password && this.game.password.trim() !== '') {
                this.hasPassword = true;
              }

            // Llama al servicio cada 5 segundos
            this.subscription = interval(5000).subscribe(() => {



                this.getRound();


                this.dataService.getGame(this.game).subscribe({
                    next: (response: any) => {
                        // Actualiza los jugadores sin recargar la página
                        this.updatePlayers(response);

                    },
                    error: (error: any) => {
                        console.log(error);
                    }
                });

                //     // Actualizar rondas cada 5 segundos
                // this.dataService.getAllRounds(this.payload).subscribe({
                //     next: (response: any) => {
                //         this.updateRounds(response);
                //     },
                //     error: (error: any) => {
                //         console.log('Error al obtener las rondas:', error);
                //     }
                // });

              //  console.log('RONDAS DATA: ' + this.payload.id + ' ' + this.payload.owner + ' ' + this.payload.password);

            });
        } else {
            this.gameResponse = null;
            this.players = [];
           // this.roundResponse = null;
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


    getRound() {

        console.log('PAYLOAD: ' + this.gameResponse);

        //si tiene password, añadirlo al payload
        if(this.hasPassword) {
            this.roundPayload.password = this.game.password;
        }


        this.dataService.getRound(this.roundPayload).subscribe({
            
            next: (response: any) => {
                this.roundResponse = response; // Actualiza la ronda
                localStorage.setItem('RoundResponse', JSON.stringify(response.data)); // Guarda la ronda en localStorage
                console.log('Round obtained:', this.roundResponse);
                this.cdr.detectChanges(); // Actualiza la vista
            },
            error: (error: any) => {
                console.error('Error al obtener la ronda:', error);
            }
        });


    }


    
  
    // // Método para obtener todas las rondas de un juego
    // getAllRounds() {
       

    //     // si tiene password, añadirlo al payload
    //     if(this.hasPassword) {
    //         this.payload.password = this.game.password;
    //     }

    //     this.dataService.getAllRounds(this.payload).subscribe({
    //         next: (data: any) => {
    //             this.rounds = data; // Almacena las rondas en la propiedad
    //             console.log('Rondas obtenidas:', this.rounds);
    //             this.cdr.detectChanges(); // Actualiza la vista
    //         },
    //         error: (error: any) => {
    //             console.error('Error al obtener las rondas:', error);
    //         }
    //     });
    // }

    // updateRounds(response: any) {
    //     this.rounds = response.data.rounds; // Actualiza la lista de rondas
    //     localStorage.setItem('Rounds', JSON.stringify(response.data.rounds)); // Guarda las rondas en localStorage
    //     console.log('Rounds updated:', this.rounds);
    //     this.cdr.detectChanges(); // Actualiza la vista
    // }

    // updateRound(response: any) {
    //     this.rounds = response.data.rounds; // Actualiza la lista de rondas
    //     localStorage.setItem('RoundResponse', JSON.stringify(response.data)); // Guarda las rondas en localStorage
    //     console.log('Rounds updated:', this.rounds);
    //     this.cdr.detectChanges(); // Actualiza la vista
    // }

}
