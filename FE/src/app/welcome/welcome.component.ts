import { Component } from '@angular/core';
import { RoomComponent } from '../pages/Room/room.component';
import { CreateRoomComponent } from '../pages/createRoom/createRoom.component';
import { EndGameComponent } from '../pages/endGame/endGame.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ChangeBEComponent } from '../pages/change-be/change-be.component';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [RoomComponent, CreateRoomComponent, EndGameComponent,ChangeBEComponent ],
  templateUrl: './welcome.component.html',
  styleUrl: './welcome.component.css'
})
export class WelcomeComponent {
  constructor() {
    if (typeof window !== 'undefined') {
      localStorage.clear();
    }
  }
}
