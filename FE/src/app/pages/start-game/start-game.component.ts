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

  name = 'StartGameComponent';

  constructor(
    private datasvc : DataService,
  ) { }

  payload = {
    id: '66ecb3055a6527506bf3869e',
    player: 'jean',
    password: 'Shazam!'
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
