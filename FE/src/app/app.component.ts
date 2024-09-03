import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {UserCardComponent} from './pages/userCard/userCard.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, UserCardComponent],
  template: `<div>
    <h1>ContaminaDos 👾</h1>
    <userCard></userCard>
    </div>`,
  // templateUrl: './app.component.html',
  // styleUrl: './app.component.css'
})



export class AppComponent {
  // title = 'contaminaDos';
}
