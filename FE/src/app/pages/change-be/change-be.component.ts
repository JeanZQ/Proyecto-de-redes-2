import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from "@angular/common";
import { LinkBE } from '../../models/app.interface';
import { DataService } from "../../services/data.service";

@Component({
  selector: 'changeBE',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatCardModule,
    CommonModule
  ],
  templateUrl: './change-be.component.html',
  styleUrl: './change-be.component.css'
})
export class ChangeBEComponent {
  
  url:string ='https://contaminados.akamai.meseguercr.com/api/games';

  linkBE: LinkBE = {
    url: this.url
  }


  myForm: FormGroup;

    constructor(
      private fb:FormBuilder,
      private dataService: DataService
    ){
      this.myForm = this.fb.group({
        newBE: ['']
      });
    }

  onSubmit() {
    if (typeof localStorage !== 'undefined') {
      this.linkBE.url = this.myForm.get('newBE')?.value;
      this.dataService.setLinkBE(this.linkBE.url);
    }
  }

  

}
