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
    voteGroup(vote: boolean) {
        this.updateVoteGroupData();
        this.voteGroupData.vote = vote;
        this.dataService.postVoteGroup(this.voteGroupData).subscribe(
            (response) => {
                vote = true;
            },
            (error) => {
                switch (error.status) {
                    case 401:
                        this._snackBar.open('The client must authenticate itself to get the requested response', 'ok', {
                            duration: 5000,
                        }); break;
                    case 403:
                        this._snackBar.open('The client does not have access rights to the content. Unlike 401 Unauthorized, the clients identity is known to the server.', 'ok', {
                            duration: 5000,
                        }); break;
                    case 404:
                        this._snackBar.open('The specified resource was not found', 'ok', {
                            duration: 5000,
                        }); break;
                    case 409:
                        this._snackBar.open('This response is sent when a request conflicts with the current state of the server.', 'ok', {
                            duration: 5000,
                        }); break;
                    case 428:
                        this._snackBar.open('The origin server requires the request to be conditional. This response is intended to prevent the (lost update) problem, where a client GETs a resources state, modifies it and PUTs it back to the server, when meanwhile a third party has modified the state on the server, leading to a conflict.', 'ok', {
                            duration: 5000,
                        }); break;
                    default:
                        this._snackBar.open('An error occurred', 'ok', {
                            duration: 5000,
                        }); break;
                }
            }
        );
    }
}
