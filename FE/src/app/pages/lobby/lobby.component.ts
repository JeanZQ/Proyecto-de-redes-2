import { NgFor, CommonModule } from "@angular/common";
import { booleanAttribute, ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, Inject, OnDestroy } from "@angular/core";
import { StartGameComponent } from "../start-game/start-game.component";
import { RoundInfoRequest, AllRoundsInfoRequest, RoundResponse, AllRoundsResponse, ServerGameResponse, StartGame, VoteGroup } from "../../models/app.interface";
import { DataService } from "../../services/data.service";
import { empty, interval, Subscription } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule, ReactiveFormsModule, FormBuilder } from "@angular/forms";
import { SelectRoundGroupComponent } from "../select-round-group/select-round-group.component";
import { MatSnackBar } from "@angular/material/snack-bar";
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { PopUpRoundInfoComponent } from "../pop-up-round-info/pop-up-round-info.component";
import { voteGroupComponent } from "../voteGroup/voteGroup.component";
import { Console } from "console";
import { NgModule } from "@angular/core";
import { MatCard } from "@angular/material/card";
import { waitForAsync } from "@angular/core/testing";
import { EndGameComponent } from "../endGame/endGame.component";
import { unescape } from "querystring";




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
        SelectRoundGroupComponent,
        MatDialogModule,
        voteGroupComponent,
        EndGameComponent

    ],
    templateUrl: './lobby.component.html',
    styleUrls: ['./lobby.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})


export class LobbyComponent implements OnDestroy {
    dialogRef: any;
    public gameResponse: string | null;
    players: any[] = []; // Lista de jugadores
    readonly gameInfo: any;
    private subscription: Subscription | null = null;
    readonly gameName: string | null | undefined;
    rounds: any[] = []; // lista de rounds
    hasPassword: boolean = false;
    enemies: string[] = [];
    private readonly _formBuiler = inject(FormBuilder);
    private roundGroup: string[] = [];
    public roundLeader: string | '' | undefined;
    public groupDefined: boolean = false;
    public leader: boolean = false;
    public gameStarted: boolean = false;
    public winner = false;
    public nameWinner = "enemies";
    public isGroupEmpty: boolean = false;
    public roundPayload: RoundInfoRequest = {
        gameId: '',
        roundId: '',
        player: ''
    };
    public groupSize:number = 0;


    // define las reglas de los grupos segun la decada y jugadores
    public groupsForDecades = [
        [2,2,3,3,3,3],
        [3,3,3,4,4,4],
        [2,4,3,4,4,4],
        [3,3,4,5,5,5],
        [3,4,4,5,5,5]
    ]


    public enemieDecades: number = 0;

    public alyDecades: number = 0;

    public gameStatus: string = '';


    readonly game: StartGame = {
        id: '',
        player: '',
        password: ''
    };

    readonly selectedPlayers = this._formBuiler.group({
        player: ''
    });

    roundResponse: RoundResponse = {
        status: 0,
        msg: '',
        data: {
            leader: '',
            status: '',
            result: '',
            phase: '',
            createdAt: '',
            updatedAt: '',
            group: [],
            votes: [],
            id: ''
        }
    }

    allRoundsResponse: AllRoundsResponse = {
        status: 0,
        msg: '',
        data: []
    }

    public allRoundsPayload: AllRoundsInfoRequest = {
        gameId: '',
        player: ''
    };

    playerVote: VoteGroup = { gameId: '', roundId: '', player: '', password: '', vote: false };

    constructor(
        private dataService: DataService,
        private cdr: ChangeDetectorRef,
        private _snackBar: MatSnackBar,
        private dialog: MatDialog
    ) {

        if (typeof localStorage !== 'undefined') {
            this.gameResponse = localStorage.getItem('GameResponse');
            this.gameInfo = localStorage.getItem('PlayerInfo');
            this.game.id = this.gameResponse ? JSON.parse(this.gameResponse).id : '';
            this.game.player = this.gameInfo ? JSON.parse(this.gameInfo).player : '';
            this.game.password = this.gameInfo ? JSON.parse(this.gameInfo).password : '';
            this.players = this.gameResponse ? JSON.parse(this.gameResponse).players : [];
            this.gameName = this.gameResponse ? JSON.parse(this.gameResponse).name : '';
            let roundId = this.gameResponse ? JSON.parse(this.gameResponse).currentRound : '';
            this.gameStatus = this.gameResponse ? JSON.parse(this.gameResponse).status : '';


            this.roundPayload = {
                gameId: this.game.id,
                roundId: roundId,
                player: this.game.player
            };

            this.allRoundsPayload = {
                gameId: this.game.id,
                player: this.game.player
            };



            // verifica si tiene password
            if (this.game.password && this.game.password.trim() !== '') {
                this.hasPassword = true;
            }



            // Llama al servicio cada 5 segundos

            this.subscription = interval(3000).subscribe(() => {
                // console.log('Game:' + localStorage.getItem('RoundResponse'));
                // console.log('ID ROUND:' + this.roundResponse.data.id);
                this.dataService.getGame(this.game).subscribe({
                    next: (response: any) => {

                      //  console.log('DECADA: ',response.data.decade);
                      //   console.log('TOTAL JUGADORES: ',response.data.players.length);
                        // console.log('Response de getGame');
                        // console.log(response);

                        // Actualiza los jugadores sin recargar la página                        

                        this.gameStatusChange(response);
                        if (this.gameStatus == 'lobby') {
                            this.updatePlayers(response);
                            this.gameStarted = false;
                        } else if (this.gameStatus == 'rounds') {
                            this.setRoundId(response.data.currentRound);
                            if (this.gameResponse) {
                                this.roundPayload = {
                                    gameId: this.game.id,
                                    roundId: response.data.currentRound,
                                    player: this.game.player
                                };
                                // console.log("Variables de la ronda");
                                // console.log(this.roundPayload);

                                // console.log("Payload de la ronda");
                                // console.log(this.gameResponse);
                            }

                            if(!this.isGroupEmpty && this.roundGroup.length != 0 && response.data.status === 'waiting-on-leader'){
                                this.roundGroup.splice(0, this.roundGroup.length);
                            }
                            

                            this.updatePlayers(response);
                            // this.isCurrentPlayerEnemy();
                            this.getRound();



                            if(response.data.currentRound === "0000000000000000000000000" ){
                                // console.log('Decade undefined');
                                this.playerOnGroup(1, response.data.players.length);
                            }
                            else{
                                this.playerOnGroup(this.allRoundsResponse.data.length-1, response.data.players.length);

                            }
                         


                        }
                        else if (this.gameStatus == 'ended') {
                            // console.log('Game ended');
                            this._snackBar.open('El juego ha terminado', 'Ok', {
                                duration: 5000,
                            });
                            this.ngOnDestroy();
                            this.winner = true;
                            this.nameWinner = Object.entries(this.allRoundsResponse.data.map((round) => round.result).reduce((acc: { [key: string]: number }, curr: string) => {
                                acc[curr] = (acc[curr] || 0) + 1;
                                return acc;
                            }, {})).reduce((a, b) => a[1] > b[1] ? a : b)[0];
                            // console.log('Winner:', this.nameWinner);

                            localStorage.clear();
                        }




                        // console.log("Actualizar ronda, jugador y juego");
                        // console.log(this.roundPayload);
                    },
                    error: (error: any) => {
                        this._snackBar.open(error.msg, 'ok', {
                            duration: 5000,
                        });
                    }
                });
                this.getAllRounds();
                // console.log('TODAS LAS RONDAS: ', this.allRoundsResponse);

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
        this.cdr.detectChanges(); // Actualiza la vista

        this.enemies = response.data.enemies; // Suponiendo que los enemigos están en response.data.enemies

        if (!this.leader && this.game.player == response.data.owner) {
            this.leader = true;
        }
        // console.log('Status: ' + response.data.status);
        if (!this.gameStarted && response.data.status === "rounds") {
            this.gameStarted = true;
        }

        // if (this.isCurrentPlayerEnemy()) {
        //     // console.log(`${this.game.player} es un enemigo.`);
        // } else {
        //     // console.log(`${this.game.player} NO es un enemigo.`);
        // }

    }

    gameStatusChange(response: any) {
        this.gameStatus = response.data.status;
        this.cdr.detectChanges();
    }

    setRoundId(roundId: string) {
        this.roundPayload.roundId = roundId;
    }




    getRound() {
        //si tiene password, añadirlo al payload
        if (this.hasPassword) {
            this.roundPayload.password = this.game.password;
        }
        // if(this.roundPayload.player == undefined) {
        //    this.roundPayload.player = this.gameResponse ? JSON.parse(this.gameResponse).player : '';
        //    this.game.player = this.gameResponse ? JSON.parse(this.gameResponse).player : '';
        // }

        this.dataService.getRound(this.roundPayload).subscribe({

            next: (response: RoundResponse) => {

                this.roundResponse = response; // Actualiza la ronda
                localStorage.removeItem('RoundResponse'); // Elimina la ronda anterior
                localStorage.setItem('RoundResponse', JSON.stringify(response.data)); // Guarda la ronda en localStorage
                this.roundLeader = response.data.leader; // Actualiza el líder de la ronda
                this.cdr.detectChanges(); // Actualiza la vista

                // console.log('Actualizando ronda');
                // console.log(response);

                if (response.data.status === 'waiting-on-leader') {
                    this.groupDefined = false;
                } else {
                    this.groupDefined = true;
                }

                // console.log('Grupo definido?:', this.groupDefined);

                this.gameStarted = true;


               
                if (response.data.result == 'citizens' || response.data.result == 'enemies') {
                    window.location.reload();
                }

            },
            error: (error: any) => {
                // console.log('Round payload:', this.roundPayload);
                this._snackBar.open(error.msg, 'ok', {
                    duration: 5000,
                });
            }
        });


    }

    currentPlayerOnGroup(player: string): boolean {
        return this.roundResponse.data.group.includes(player);
    }


    // retorna todos los rounds de un juego
    getAllRounds() {

        // verifica si tiene password y la añade al payload
        if (this.hasPassword) {
            this.allRoundsPayload.password = this.game.password;
        }

        this.dataService.getAllRounds(this.allRoundsPayload).subscribe({

            next: (response: any) => {
                this.allRoundsResponse = response;
                this.cdr.detectChanges();
                // console.log('All rounds:', response);
            },
            error: (error: any) => {
                console.error('Error getting all rounds:', error);
            }
        });
    }

    // devuelve si el player actual es enemigo
    isCurrentPlayerEnemy(): boolean {
        return this.enemies.includes(this.game.player);
    }

    returnPlayerName():string{
        return this.game.player;
    }


    onPlayerSelect(player: string, event: boolean) {
        // console.log('Player selected:', player, event);

        if (event) {
            this.roundGroup.push(player);
        } else {
            this.roundGroup = this.roundGroup.filter(p => p !== player);
        }
    }


    selectGroup() {
        // console.log('Round group:', this.roundGroup);

        const payload = {
            gameId: this.roundPayload.gameId,
            roundId: this.roundPayload.roundId,
            player: this.roundPayload.player,
            password: this.game.password || '',
            group: this.roundGroup
        };

        // console.log('Proposing group:', payload);

        this.dataService.proposeGroup(payload).subscribe({
            next: (response: any) => {
                this._snackBar.open('Grupo propuesto', 'ok', {
                    duration: 5000,
                  });
                
            },
            error: (e: any) => {
                switch (e.status) {
                    case 401:
                      this._snackBar.open('Credenciales inválidas', 'Ok', {
                        duration: 5000,
                      });
                      break;
          
                    case 403:
                      this._snackBar.open('No eres parte del juego', 'Ok', {
                        duration: 5000,
                      });
                      break;
          
                    case 404:
                      this._snackBar.open('Juego no encontrado', 'Ok', {
                        duration: 5000,
                      });
                      break;
                    case 409:
                        this._snackBar.open('Ya se creo el grupo', 'Ok', {
                          duration: 5000,
                        });
                        break;
                    case 428:
                      this._snackBar.open('Tamaño incorrecto del grupo', 'Ok', {
                        duration: 5000,
                      });
                      break;
                  }
            }
        })
    }

    voting(vote: boolean) {
        this.playerVote = {
            gameId: this.roundPayload.gameId,
            roundId: this.roundPayload.roundId,
            player: this.roundPayload.player,
            password: this.game.password || '',
            vote: vote
        };
        this.dataService.votePlayer(this.playerVote).subscribe({
            next: (response: ServerGameResponse) => {
                // console.log('Voto:', response.data);
                if (response.status == 200) {
                    if (this.playerVote.vote == true) {
                        this._snackBar.open('Has apoyado', 'Ok', {
                            duration: 5000,
                        });
                    } else {
                        this._snackBar.open('Has saboteado', 'Ok', {
                            duration: 5000,
                        });

                    }
                }
            },
            error: (e) => {
                switch (e.status) {
                    case 401:
                      this._snackBar.open('Credenciales invalidas', 'Ok', {
                        duration: 5000,
                      });
                      break;
          
                    case 403:
                      this._snackBar.open('No eres parte del juego', 'Ok', {
                        duration: 5000,
                      });
                      break;
          
                    case 404:
                      this._snackBar.open('Juego no encontrado', 'Ok', {
                        duration: 5000,
                      });
                      break;

                    case 409:
                        this._snackBar.open('Ya hiciste una acción', 'Ok', {
                          duration: 5000,
                        });
                        break;
          
                    case 428:
                      this._snackBar.open('No puedes hacer eso en este momento', 'Ok', {
                        duration: 5000,
                      });
                      break;

                  }
            }
        });

    }

    // muestra de cuantos players debe ser el grupo segun la ronda
    playerOnGroup(decade: number, totalPlayers: number) {
        
        // el indice se acomoda a una columna de la matriz
        const playersIndex = totalPlayers - 5;
        // this.groupSize = this.groupsForDecades[decade - 1][playersIndex];
        // console.log('Grupo:', playersIndex);
        // console.log('Decada:', decade);

        // console.log(this.groupsForDecades [decade][playersIndex]);

        this.groupSize = this.groupsForDecades[decade][playersIndex];

        // return this.groupsForDecades [decade - 1][playersIndex];
    }
    


    // pop up para mostrar el round 
    openDialog() {
        const dialogRef = this.dialog.open(PopUpRoundInfoComponent, {
            data: this.roundResponse,
            width: '80%',     // 80% del ancho de la pantalla
            height: '70vh',   // 70% de la altura del viewport
            maxWidth: '600px', // Ancho máximo
            maxHeight: '500px'
        });


    }

}
