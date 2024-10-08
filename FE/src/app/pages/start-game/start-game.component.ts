import { Component } from '@angular/core';
import { MatCard, MatCardModule } from '@angular/material/card';
import { DataService } from '../../services/data.service';
import { MatButton } from '@angular/material/button';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'startGame',
  standalone: true,
  imports: [MatCard, MatCardModule, MatButton],
  templateUrl: './start-game.component.html',
  styleUrl: './start-game.component.css'
})
export class StartGameComponent {

  public leader: boolean = false;

  constructor(
    private datasvc: DataService,
    private _snackBar: MatSnackBar
  ) { }


  // Cambiar el payload para que tome los valores del local storage y solo se haga cuando es el dueño
  payload = {
    id: localStorage.getItem('GameResponse') ? JSON.parse(localStorage.getItem('GameResponse') as string).id : '',
    player: localStorage.getItem('GameResponse') ? JSON.parse(localStorage.getItem('GameResponse') as string).owner : '',
    password: localStorage.getItem('PlayerInfo') ? JSON.parse(localStorage.getItem('PlayerInfo') as string).password : ''
  };


  startGame() {
    this.datasvc.startGame(this.payload).subscribe({
      next: (response: any) => {
        localStorage.setItem('GameResponse', JSON.stringify(response.data));
        // window.location.reload();
      },
      error: (e) => {
        switch (e.status) {
          case 401:
            this._snackBar.open('No autorizado', 'Ok', {
              duration: 5000,
            });
            break;

          case 403:
            this._snackBar.open('Prohibido', 'Ok', {
              duration: 5000,
            });
            break;

          case 404:
            this._snackBar.open('Juego no encontrado', 'Ok', {
              duration: 5000,
            });
            break;

          case 428:
            this._snackBar.open('Se necesitan al menos 5 jugadores para empezar', 'Ok', {
              duration: 5000,
            });
            break;
        }
      }
    });
  }
}
