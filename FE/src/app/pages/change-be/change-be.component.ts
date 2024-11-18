import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from "@angular/common";

@Component({
  selector: 'changeBE',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatCardModule
  ],
  templateUrl: './change-be.component.html',
  styleUrl: './change-be.component.css'
})
export class ChangeBEComponent {
  
  url:string ='https://contaminados.akamai.meseguercr.com/api/games';

  ngOnInit() {

  }


  onSubmit() {
    console.log('onSubmit');
  }

}
