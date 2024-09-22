import { Routes } from '@angular/router';
import { CreateRoomComponent } from './pages/createRoom/createRoom.component';
import { RoomComponent } from './pages/Room/room.component';
import { LobbyComponent } from './pages/lobby/lobby.component';
import { WelcomeComponent } from './welcome/welcome.component';

export const routes: Routes = [
    {path:'',component:WelcomeComponent},
    // {path:'createRoom',component:CreateRoomComponent},
    {path:'lobby',component:LobbyComponent}
];
