import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserCardComponent } from './pages/userCard/userCard.component';
import { RoomComponent } from "./pages/Room/room.component";
import { JsonPipe } from '@angular/common';
import { CreateRoomComponent } from './pages/createRoom/createRoom.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { getRoomComponent } from "./pages/getRoom/getRoom.component";
import { getRoomInputComponent } from "./pages/getRoom/getRoomInput.component";
import { MatGridListModule } from '@angular/material/grid-list';

@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.css'],
  templateUrl: './app.component.html',
  standalone: true,
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
    getRoomComponent,
    getRoomInputComponent,
    MatGridListModule,
  ]
})
export class AppComponent {}
