import { Component, OnInit} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { DataService } from '../../services/data.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // Necesario para Angular Material
import { MatSnackBar } from '@angular/material/snack-bar';
import { inject } from '@angular/core';

@Component({
  selector: 'createRoom',
  standalone: true,
  templateUrl: './createRoom.component.html',
  styleUrl: './createRoom.component.css',
  imports: [MatCardModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule]

})
export class CreateRoomComponent{
    
    myForm: FormGroup;
    messages: string[] = ["Se creo la sala","No se pudo crear la sala", "Ya existe una sala con ese nombre" ];

    constructor(
        private datasvc : DataService, 
        private fb: FormBuilder, 
        private _snackBar: MatSnackBar
    ) {
      this.myForm = this.fb.group({
        name: ['', [Validators.required]],
        owner: ['', [Validators.required]],
        password: ['']
      });
    }
    
    onSubmit() {
      if (this.myForm.valid) {
        console.log('Formulario enviado', this.myForm.value);
        this.createGame();
      } else {
        this._snackBar.open('Formulario Invalido', 'Ok', {
            duration: 5000,
        });
      }
    }
    
    createGame(){
        console.log('Creating new game');
        let response: number = 0;
        let messages: string = 'Ocurrio un error';
        const newGame = {
            name: this.myForm.value.name,
            owner: this.myForm.value.owner,
            password: this.myForm.value.password
        }
        this.datasvc.createRoom(newGame).subscribe(
            data => (response = data.status)
        );

        if(response == 200){
            messages = "Se creo la sala";
        }else if(response == 409){
            messages = "Ya existe una sala con ese nombre";
        }else if(response == 400){
            messages = "Ocurrio un error";
        }

        this._snackBar.open(messages, 'Ok', {
            duration: 5000,
        });
}
    


}