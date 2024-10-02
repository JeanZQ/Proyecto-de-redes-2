import { Component } from "@angular/core";
import { GameResponse, VoteGroup } from "../../models/app.interface";
import { MatSnackBar } from "@angular/material/snack-bar";
import { DataService } from "../../services/data.service";

@Component({
    selector: 'voteGroup',
    standalone: true,
    imports: [
    ],
    templateUrl: './voteGroup.component.html',
    styleUrl: './voteGroup.component.css'
})
export class voteGroupComponent {
    private local: GameResponse = JSON.parse(localStorage.getItem('RoundResponse') || '{}') as GameResponse;
    private voteGroupData: VoteGroup = {
        gameId: this.local.id,
        roundId: this.local.currentRound,
        password: "123",
        player: this.local.owner,
        vote: false
    };
    constructor(private dataService: DataService, private _snackBar: MatSnackBar) { }
    updateVoteGroupData(){
        this.voteGroupData = {
            gameId: this.local.id,
            roundId: this.local.currentRound,
            password: "123",
            player: this.local.owner,
            vote: false
        };
    }
    voteGroup(vote: boolean) {
        this.updateVoteGroupData();
        this.voteGroupData.vote = vote;
        this.dataService.postVoteGroup(this.voteGroupData).subscribe(
            (response) => {
                console.log(response);
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
