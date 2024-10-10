import { Component, Input, input } from "@angular/core";
import { GameResponse, RoundInfoRequest, VoteGroup } from "../../models/app.interface";
import { MatSnackBar } from "@angular/material/snack-bar";
import { DataService } from "../../services/data.service";
import { ComponentFixture } from "@angular/core/testing";
import { CommonModule } from "@angular/common";

@Component({
    selector: 'voteGroup',
    standalone: true,
    imports: [
        CommonModule
    ],
    templateUrl: './voteGroup.component.html',
    styleUrl: './voteGroup.component.css'
})
export class voteGroupComponent {
    @Input() roundPayload: RoundInfoRequest = {
        gameId: '',
        roundId: '',
        player: ''
    };
    @Input() password = '';
    @Input() group: string[] = [];
    public voting : boolean = false;
    private voteGroupData: VoteGroup = {
        gameId: '',
        roundId: '',
        password: '',
        player: '',
        vote: false
    };
    constructor(private dataService: DataService, private _snackBar: MatSnackBar) {

    }
    updateVoteGroupData() {
        this.voteGroupData = {
            gameId: this.roundPayload.gameId,
            roundId: this.roundPayload.roundId,
            password: this.password,
            player: this.roundPayload.player,
            vote: false
        };
    }
    voteGroup(vote: boolean, event: Event) {
        event.preventDefault();
        event.stopPropagation();
        this.updateVoteGroupData();
        // console.log('VOTE GROUP DATA ESTADO', this.voteGroupData);
        this.voteGroupData.vote = vote;
        this.dataService.postVoteGroup(this.voteGroupData).subscribe(
            (response) => {
                if(response.status == 200) {
                    this.voting = true;
                    this._snackBar.open('Votación en progreso', 'ok', {
                        duration: 5000,
                    });
                }
            },
            (error) => {
                switch (error.status) {
                    case 401:
                        this._snackBar.open('Jugador no autenticado', 'ok', {
                            duration: 5000,
                        }); break;
                    case 403:
                        this._snackBar.open('No tienes permiso de hacer eso en este momento', 'ok', {
                            duration: 5000,
                        }); break;
                    case 404:
                        this._snackBar.open('No se encontro el recurso', 'ok', {
                            duration: 5000,
                        }); break;
                    case 409:
                        this._snackBar.open('ERROR: conflictos con el estado del servidor.', 'ok', {
                            duration: 5000,
                        }); break;
                    case 428:
                        this._snackBar.open('Conflicto con el servidor', 'ok', {
                            duration: 5000,
                        }); break;
                    default:
                        this._snackBar.open('Ocurrio un error', 'ok', {
                            duration: 5000,
                        }); break;
                }
            }
        );
    }
}
