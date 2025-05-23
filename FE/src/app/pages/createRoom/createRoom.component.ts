import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { DataService } from '../../services/data.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RouterLink } from '@angular/router';
import { DEFAULT_PASSWORD } from '../../models/app.interface';

@Component({
  selector: 'createRoom',
  standalone: true,
  templateUrl: './createRoom.component.html',
  styleUrl: './createRoom.component.css',
  imports: [MatCardModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    RouterLink
  ]

})

export class CreateRoomComponent {

  myForm: FormGroup;

  constructor(
    private datasvc: DataService,
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


    createRoom(){
      const { name, owner, password } = this.myForm.value;

      // Crear el payload
      const newRoom: any = {
        name,
        owner,
        player: owner,
      };

      // Añadir la contraseña solo si no está vacía
      if(password && password.trim() !== '') {
      newRoom.password = password;
    }

    // console.log(newRoom);

    // Enviar la información al servicio
    this.datasvc.createRoom(newRoom).subscribe({
      

      next: (response: any) => {
        localStorage.setItem('GameResponse', JSON.stringify(response.data));
        localStorage.setItem('PlayerInfo', JSON.stringify(newRoom));
        this._snackBar.open('Sala creada', 'ok', {
          duration: 5000,
        });
        window.location.href = '/lobby';
      },
      error: (e: any) => {
        switch (e.status) {
          case 400:
            this._snackBar.open('Error del cliente', 'Ok', {
              duration: 5000,
            });
            break;

          case 409:
            this._snackBar.open('Un juego con ese nombre ya existe', 'Ok', {
              duration: 5000,
            });
            break;
        }
      }
    });
  }

}


