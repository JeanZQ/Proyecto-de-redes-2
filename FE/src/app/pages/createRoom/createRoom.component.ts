import { Component, OnInit} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'createRoom',
  template: '<button mat-raised-button  (click)="onSubmit()">Create Room</button>',
  standalone: true,
  imports: [MatCardModule, MatButtonModule],
})
export class CreateRoomComponent implements OnInit {
    
    constructor(private datasvc : DataService) {
    }

    ngOnInit() {
        this.onSubmit();
    };

    onSubmit(){
        console.log('Creating new game2');
        const newGame = {
            name: 'Prueba Angular Chuta',
            owner: 'Chuta',
            password: 'Chuta'
        }
        this.datasvc.createRoom(newGame).subscribe(
            data => (console.log(data))
        );
    };
}