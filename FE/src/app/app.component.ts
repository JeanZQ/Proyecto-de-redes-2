import { Component} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {UserCardComponent} from './pages/userCard/userCard.component';
import { RoomComponent } from "./pages/room/room.component";
import { JsonPipe } from '@angular/common';
import { CreateRoomComponent } from './pages/createRoom/createRoom.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, UserCardComponent, RoomComponent,
     JsonPipe,CreateRoomComponent],
  template: `<div>
    <h1>ContaminaDos 👾</h1>
    <createRoom></createRoom>
    <room></room>

    </div>`,

})



export class AppComponent {



}
