import { ChangeDetectionStrategy, Component, OnInit } from "@angular/core";
import { ServerGameResponse } from "../../models/app.interface";
import { UserRoomComponent } from '../userRoom/userRoom.component';
import { JsonPipe, NgFor, CommonModule } from "@angular/common";
import { DataService } from "../../services/data.service";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatListModule } from '@angular/material/list';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
    selector: 'room',
    standalone: true,
    imports: [
        UserRoomComponent,
        NgFor,
        JsonPipe,
        MatButtonModule,
        MatCardModule,
        MatListModule,
        CommonModule,
        FormsModule,
        MatInputModule,
        MatFormFieldModule
    ],
    templateUrl: './room.component.html',
    styleUrls: ['./room.component.css'],
})

export class RoomComponent implements OnInit {

    serverData!: ServerGameResponse;
    searchTerm: string = '';
    filteredResults: any[] = [];

    constructor(private datasvc: DataService) { }

    ngOnInit() {
        this.datasvc.getRooms().subscribe(
            response => {
                this.serverData = response;
                this.filteredResults = this.serverData.data;
            },
        );
    }

    searchRoom() {
        if (!this.searchTerm) {
            this.filteredResults = this.serverData.data;
        } else {
            this.filteredResults = this.serverData.data.filter(room =>
                room.name.toLowerCase().includes(this.searchTerm.toLowerCase())
            );
        }
    }
}
