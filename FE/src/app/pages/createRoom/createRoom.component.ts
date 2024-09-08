import { Component, OnInit} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'createRoom',
  template: '<button mat-raised-button  (click)="createGame()">Create Room</button>',
  standalone: true,
  imports: [MatCardModule, MatButtonModule],
})
export class CreateRoomComponent implements OnInit {
    
    constructor(private datasvc : DataService) {
    }

    ngOnInit() {
        this.createGame();
    };

    createGame(){
        console.log('Creating new game2');
    //     const newGame = {
    //         name: 'Prueba Angular Chuta',
    //         owner: 'Chuta',
    //         password: 'Chuta'
    //     }
    //     this.datasvc.createRoom(newGame).subscribe(
    //         data => (console.log(data))
    //     );
    };

    // searchGame(){
    //     this.datasvc.getGame({id: '66de0a8f5a6527506bd6b15c', owner: 'Chuta', password: 'Chuta'}).subscribe(
    //         data => (console.log(data))
    //     );
    // };

    // joinGame(){
    //     this.datasvc.joinGame({id: '66de0a8f5a6527506bd6b15c', owner: 'Luis', password: 'Chuta', player: 'Luis'}).subscribe(
    //         data => (console.log(data
    //         ))
    //     );
    // };



}