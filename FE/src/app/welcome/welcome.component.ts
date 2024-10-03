import { Component } from '@angular/core';
import { RoomComponent } from '../pages/Room/room.component';
import { CreateRoomComponent } from '../pages/createRoom/createRoom.component';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [RoomComponent,CreateRoomComponent],
  templateUrl: './welcome.component.html',
  styleUrl: './welcome.component.css'
})
export class WelcomeComponent {
  constructor() {
    localStorage.clear();
  }
}
