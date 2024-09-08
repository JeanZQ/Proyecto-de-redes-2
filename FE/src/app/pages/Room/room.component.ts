import { Component, Input, OnInit } from "@angular/core";
import { ServerGameResponse,GameResponse } from "../../models/app.interface";
import { UserRoomComponent } from '../userRoom/userRoom.component';
import { JsonPipe, NgFor,CommonModule } from "@angular/common";
import { DataService } from "../../services/data.service";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import {MatListModule} from '@angular/material/list';

@Component({
    selector: 'room',
    standalone: true,
    imports: [UserRoomComponent,NgFor,JsonPipe,MatButtonModule,
        MatCardModule, MatButtonModule,MatListModule,CommonModule],
    templateUrl: './room.component.html',
    styleUrl: './room.component.css',
})

export class RoomComponent implements OnInit {
    
    serverData! : ServerGameResponse;

    constructor(private datasvc : DataService) {
    }
    
    ngOnInit() {
        this.datasvc.getRooms().subscribe(
            response => (this.serverData = response),
        );
        // this.createRoom();
    };

    createRoom(){
        console.log('Creating new game');
        const newGame = {
            name: 'Prueba Angular Raul',
            owner: 'Raul',
            password: 'Raul'
        }
        this.datasvc.createRoom(newGame).subscribe(
            data => (console.log(data))
        );
    };
}