import { Component, Input, OnInit } from "@angular/core";
import { Server } from "http";
import { ServerResponse,GameResponse } from "../../models/serverResponse";
import { UserRoomComponent } from '../userRoom/userRoom.component';
import { JsonPipe, NgFor } from "@angular/common";

@Component({
    selector: 'room',
    standalone: true,
    imports: [UserRoomComponent,NgFor,JsonPipe],
    templateUrl: './room.component.html',
    styleUrl: './room.component.css',
})

export class RoomComponent {
    
    @Input() serverData: ServerResponse[] = [];
    rooms: GameResponse[] = [];
    names: string[] = [];
    owner: string[] = [];
    constructor() {     
    }
    
    ngOnInit() {
        this.getRooms();
        // console.log("Respuesta del server 1" + this.serverData)
    };

    
    public getRooms() {
        // if (this.serverData && this.serverData.data && this.serverData.data.length > 0) {
        //     this.rooms = this.serverData.data;

        //     // Guardar los nombres y propietarios en arrays separados
        //     for (let room of this.rooms) {
        //         this.names.push(room.name);
        //     //     this.owners.push(room.owner);  // Corrigiendo typo, "owner" -> "owners"
        //     }

        //     console.log("Nombres: ", this.names);
        //     // console.log("Propietarios: ", this.owners);
        // } else {
        //     console.log("No hay datos disponibles en serverData.");
        // }

        console.log(this.serverData)
    }

    
}