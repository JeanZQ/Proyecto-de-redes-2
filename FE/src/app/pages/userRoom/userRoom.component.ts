import { Component, ChangeDetectionStrategy, Input, OnInit } from "@angular/core";
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';

@Component ({
    selector: 'userRoom',
    imports: [MatButtonModule, MatCardModule],
    standalone: true,
    templateUrl: './userRoom.component.html',
    styleUrl: './userRoom.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
    
    })

export class UserRoomComponent  {

    @Input() name: string = "";
    // owner: string;

    constructor() {};

    ngOnInit() {};

}