import { Component } from '@angular/core';
import { MatCard, MatCardModule } from '@angular/material/card';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'startGame',
  standalone: true,
  imports: [MatCard, MatCardModule],
  templateUrl: './start-game.component.html',
  styleUrl: './start-game.component.css'
})
export class StartGameComponent {

  name = 'StartGameComponent';

  constructor(
    private datasvc : DataService,
  ) { }

  startGame(){
    console.log('Starting game');
  }


}
