import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {UserCardComponent} from './pages/userCard/userCard.component';
import { HttpClient } from '@angular/common/http';
import { response } from 'express';
import { ServerGameResponse } from './models/app.interface';
import { RoomComponent } from "./pages/Room/room.component";
import { Server } from 'net';
import { JsonPipe } from '@angular/common';
import { Observable } from 'rxjs';
import { get, ServerResponse } from 'http';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, UserCardComponent, RoomComponent, JsonPipe],
  template: `<div>
    <h1>ContaminaDos 👾</h1>
    <!-- <userCard></userCard> -->
    <room></room>
    <!-- <p>{{sData | json}}</p> -->
    </div>`,
  // templateUrl: './app.component.html',
  // styleUrl: './app.component.css'
})



export class AppComponent {



}
