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
    MatFormFieldModule
  ]

})

export class CreateRoomComponent{
    
    myForm: FormGroup;

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
    
      const isFormValid = this.myForm.get('name')?.valid && this.myForm.get('owner')?.valid;
      if (isFormValid) {
        
        this.createRoom();
        
        
        this.myForm.reset({
          name: '',
          owner: '',
          password: ''
        });
        
        // // Establece el estado de validez sin errores 
        // Object.keys(this.myForm.controls).forEach(control => {
        //   this.myForm.controls[control].setErrors(null);
        // });
        

      } else {

        this._snackBar.open('Formulario Invalido', 'Ok', {
            duration: 5000,
        });
      }
    }
  

    // crea la sala
    createRoom() {
      const newRoom = {
        name: this.myForm.value.name,
        owner: this.myForm.value.owner,
        password: this.myForm.value.password
      };

      this.datasvc.createRoom(newRoom).subscribe(() => {
        this._snackBar.open('Sala creada', 'ok', 
          { duration: 5000

          });
        });
      }

  
 }
  
    
  

  

   
 

