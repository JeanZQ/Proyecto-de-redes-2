import { Component, Input } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-select-round-group',
  standalone: true,
  imports: [MatButton],
  templateUrl: './select-round-group.component.html',
  styleUrl: './select-round-group.component.css'
})
export class SelectRoundGroupComponent {

  @Input() roundGroup:[] = [];

  constructor() { }

  selectGroup(){
    console.log(this.roundGroup);
  }

}
