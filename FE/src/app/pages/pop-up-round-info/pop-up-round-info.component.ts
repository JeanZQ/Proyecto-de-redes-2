import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RoundResponse } from '../../models/app.interface';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-pop-up-round-info',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pop-up-round-info.component.html',
  styleUrl: './pop-up-round-info.component.css'
})
export class PopUpRoundInfoComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: RoundResponse) {}

}
