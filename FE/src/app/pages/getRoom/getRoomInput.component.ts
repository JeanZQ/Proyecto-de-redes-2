import {Component} from '@angular/core';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {FormsModule} from '@angular/forms';

/**
 * @title Basic Inputs
 */
@Component({
  selector: 'getRoomInput',
  styleUrl: './getRoomInput.component.css',
  templateUrl: './getRoomInput.component.html',
  standalone: true,
  imports: [FormsModule, MatFormFieldModule, MatInputModule],
})
export class getRoomInputComponent {}
