import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { RoomComponent } from "./pages/Room/room.component";
import { JsonPipe } from '@angular/common';
import { CreateRoomComponent } from './pages/createRoom/createRoom.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { LobbyComponent } from './pages/lobby/lobby.component';
import { StartGameComponent } from './pages/start-game/start-game.component';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./responsive.web.design.css'],
  imports: [
    RouterOutlet,
    RoomComponent,
    JsonPipe,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatCardModule,
    CreateRoomComponent,
    MatGridListModule,
    LobbyComponent,
    StartGameComponent,
    RouterLink,
  ]
})
export class AppComponent {
}
