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
import { MatSnackBar } from "@angular/material/snack-bar";

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
    constructor(
        private datasvc: DataService,
        private _snackBar: MatSnackBar
    ) { }

    ngOnInit() {
        this.currentPage = 0;
        this.pagination(this.currentPage);
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
                    this._snackBar.open('Error buscando la sala', 'ok', {
                        duration: 5000,
                      });
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

    pagination(page: number) {
        this.currentPage = page;
        this.datasvc.getRooms(this.currentPage, this.pageSize).subscribe(
            response => {
                this.serverData = response;
                this.filteredResults = this.serverData.data;
            },
            error => {
                this._snackBar.open('Error cargando las páginas', 'ok', {
                    duration: 5000,
                  });
            }
        );
    }

}
