import { Component } from '@angular/core';
import { MatCard, MatCardModule } from '@angular/material/card';
import { DataService } from '../../services/data.service';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'startGame',
  standalone: true,
  imports: [MatCard, MatCardModule,MatButton],
  templateUrl: './start-game.component.html',
  styleUrl: './start-game.component.css'
})
export class StartGameComponent {

  public leader:boolean = false;

  constructor(
    private datasvc : DataService,
  ) { }

  
  // Cambiar el payload para que tome los valores del local storage y solo se haga cuando es el dueño
  payload = {
    id: localStorage.getItem('GameResponse') ? JSON.parse(localStorage.getItem('GameResponse') as string).id : '',
    player: localStorage.getItem('GameResponse') ? JSON.parse(localStorage.getItem('GameResponse') as string).owner : '',
    password: localStorage.getItem('PlayerInfo') ? JSON.parse(localStorage.getItem('PlayerInfo') as string).password : ''
  };

  
  startGame(){
    console.log(this.payload);
    this.datasvc.startGame(this.payload).subscribe({
      next: (response: any) => {
        console.log(response);
        localStorage.setItem('GameResponse', JSON.stringify(response.data));
      },
      error: (error: any) => {
        console.log(error);
      }
    });
  }

}
