import { Routes } from '@angular/router';
import { RoomComponent } from './pages/room/room.component';
import { CreateRoomComponent } from './pages/createRoom/createRoom.component';

export const routes: Routes = [
    {path:'',component:RoomComponent},
    {path:'createRoom',component:CreateRoomComponent},
];
