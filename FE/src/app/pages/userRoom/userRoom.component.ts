import { Component, ChangeDetectionStrategy, Input, OnInit } from "@angular/core";
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { MatIcon } from "@angular/material/icon";
import { GameResponse } from "../../models/app.interface";

@Component ({
    selector: 'userRoom',
    imports: [MatButtonModule, MatCardModule,MatIcon],
    standalone: true,
    templateUrl: './userRoom.component.html',
    styleUrl: './userRoom.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
    
    })

export class UserRoomComponent  {

    @Input()game!: GameResponse;
    constructor() {};

    ngOnInit() {};

}