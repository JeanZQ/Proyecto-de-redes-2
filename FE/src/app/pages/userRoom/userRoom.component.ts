import { Component, ChangeDetectionStrategy, Input, OnInit } from "@angular/core";
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIcon } from "@angular/material/icon";
import { GameResponse } from "../../models/app.interface";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";

@Component({
    selector: 'userRoom',
    imports: [
        MatButtonModule,
        MatCardModule,
        MatIcon,
        MatFormFieldModule, 
        MatInputModule, 
        MatButtonModule],
    standalone: true,
    templateUrl: './userRoom.component.html',
    styleUrl: './userRoom.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,

})

export class UserRoomComponent {
    @Input() game!: GameResponse;
    constructor() { };

    joinRoom() {
        console.log('Joining room');
    }

}