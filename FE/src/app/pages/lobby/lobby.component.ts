import { NgFor, CommonModule } from "@angular/common";
import { booleanAttribute, ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, Inject, OnDestroy } from "@angular/core";
import { StartGameComponent } from "../start-game/start-game.component";
import { RoundInfoRequest, RoundResponse, StartGame, VoteGroup } from "../../models/app.interface";
import { DataService } from "../../services/data.service";
import { interval, Subscription } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule, ReactiveFormsModule, FormBuilder } from "@angular/forms";
import { SelectRoundGroupComponent } from "../select-round-group/select-round-group.component";
import { MatSnackBar } from "@angular/material/snack-bar";
import { voteGroupComponent } from "../voteGroup/voteGroup.component";


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
        voteGroupComponent
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
        player: ''
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

    playerVote : VoteGroup = { gameId: '', roundId: '', player: '', password: '', vote: false };

    constructor(
        private dataService: DataService,
        private cdr: ChangeDetectorRef,
        private _snackBar: MatSnackBar
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

            // payload para obtener una ronda
            this.roundPayload = {
                gameId: this.game.id,
                roundId: roundId,
                player: this.game.player
            };



            // verifica si tiene password
            if (this.game.password && this.game.password.trim() !== '') {
                this.hasPassword = true;
            }

            // Llama al servicio cada 5 segundos
            this.subscription = interval(5000).subscribe(() => {
                this.getRound();
                console.log('Game:' + localStorage.getItem('RoundResponse'));
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
        this.cdr.detectChanges(); // Actualiza la vista
        this.enemies = response.data.enemies; // Suponiendo que los enemigos están en response.data.enemies

        if (!this.leader && this.game.player == response.data.owner) {
            this.leader = true;
        }
        console.log('Status: ' + response.data.status);
        if (!this.gameStarted && response.data.status === "rounds") {
            this.gameStarted = true;
        }

        if (this.isCurrentPlayerEnemy()) {
            console.log(`${this.game.player} es un enemigo.`);
        } else {
            console.log(`${this.game.player} NO es un enemigo.`);
        }

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

            next: (response: any) => {
                this.roundResponse = response; // Actualiza la ronda
                localStorage.setItem('RoundResponse', JSON.stringify(response.data)); // Guarda la ronda en localStorage
                this.roundLeader = response.data.leader; // Actualiza el líder de la ronda
                this.cdr.detectChanges(); // Actualiza la vista
            },
            error: (error: any) => {
                console.log('Round payload:', this.roundPayload);
                this._snackBar.open('Error al obtener la ronda', 'ok', {
                    duration: 5000,
                });
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

        const payload = {
            gameId: this.roundPayload.gameId,
            roundId: this.roundPayload.roundId,
            player: this.roundPayload.player,
            password: this.game.password || '',
            group: this.roundGroup
        };

        this.dataService.proposeGroup(payload).subscribe({
            next: (response: any) => {
                console.log('Group proposed:', response);
                this.groupDefined = true;
            },
            error: (error: any) => {
                console.error('Error proposing group:', error);
            }
        })
    }

    voting(vote: boolean) {
        console.log(`Votaste a ${this.game.player}`);
        this.playerVote = {
            gameId: this.roundPayload.gameId,
            roundId: this.roundPayload.roundId,
            player: this.roundPayload.player,
            password: this.game.password || '',
            vote: vote
        };

        this.dataService.votePlayer(this.playerVote).subscribe({
            next: (response: any) => {
                console.log('Voto:', response);
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
                    this._snackBar.open('The client must authenticate itself to get the requested response', 'Ok', {
                      duration: 5000,
                    });
                    break;
        
                  case 403:
                    this._snackBar.open('The client does not have access rights to the content. Unlike 401 Unauthorized, the clients identity is known to the server.', 'Ok', {
                      duration: 5000,
                    });
                    break;
        
                  case 404:
                    this._snackBar.open('The specified resource was not found', 'Ok', {
                      duration: 5000,
                    });
                    break;
        
                  case 409:
                    this._snackBar.open('This response is sent when a request conflicts with the current state of the server.', 'Ok', {
                      duration: 5000,
                    });
                    break;
                
                    case 429:
                    this._snackBar.open('The origin server requires the request to be conditional. This response is intended to prevent the "lost update" problem, where a client GETs a resources state, modifies it and PUTs it back to the server, when meanwhile a third party has modified the state on the server, leading to a conflict.', 'Ok', {
                      duration: 5000,
                    });
                    break;
                }
              }
        });
    }

}
