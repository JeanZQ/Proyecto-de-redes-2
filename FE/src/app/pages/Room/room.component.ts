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
import { MatPaginator } from '@angular/material/paginator';
import { RouterLink } from "@angular/router";
import { StartGameComponent } from "../start-game/start-game.component";
import { MatSelectModule } from "@angular/material/select";

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
        MatFormFieldModule,
        MatPaginator,
        RouterLink,
        StartGameComponent,
        MatSelectModule
    ],
    templateUrl: './room.component.html',
    styleUrls: ['./room.component.css', '../../responsive.web.design.css'],
})

export class RoomComponent implements OnInit {

    serverData!: ServerGameResponse;
    searchTerm: string = '';
    filteredResults: any[] = [];
    currentPage: number = 0;
    pageSize: number = 250;
    selectedOption: string = 'lobby';
    constructor(private datasvc: DataService) { }

    ngOnInit() {
        this.loadRooms(this.currentPage, this.pageSize);
    }

    loadRooms(page: number, pageSize: number) {
        this.datasvc.getRooms(page, pageSize).subscribe(
            response => {
                this.serverData = response;
                this.filteredResults = this.serverData.data;
            },
            error => {
                console.error("Error loading rooms:", error);
            }
        );
    }

    onPageChange(event: any) {
        this.currentPage = event.pageIndex;
        this.loadRooms(this.currentPage, this.pageSize);
    }

    onInput() {
        if (!this.searchTerm) {
            this.filteredResults = this.serverData.data;
        } else {
            this.searchRooms();
        }
    }

    searchRooms() {
        if (this.searchTerm.length >= 3 && this.searchTerm.length <= 20) {
            this.datasvc.gamesearch({ name: this.searchTerm, status: this.selectedOption, page: 0, limit: 50 }).subscribe(
                response => {
                    this.serverData = response;
                    this.filteredResults = this.serverData.data;
                },
                error => {
                    alert("Error searching rooms: " + error);
                }
            );
        } else {
            this.ngOnInit();
        }
    }

    selectOption(option: string) {
        this.selectedOption = option;
        this.searchRooms();
    }
}
