import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {UserCardComponent} from './pages/userCard/userCard.component';
import { HttpClient } from '@angular/common/http';
import { response } from 'express';
import { ServerResponse } from '../app/models/serverResponse';
import { RoomComponent } from "./pages/Room/room.component";
import { Server } from 'net';
import { JsonPipe } from '@angular/common';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, UserCardComponent, RoomComponent, JsonPipe],
  template: `<div>
    <h1>ContaminaDos 👾</h1>
    <userCard></userCard>
    <room [serverData] = sData></room>
    <!-- <p>{{sData | json}}</p> -->
    </div>`,
  // templateUrl: './app.component.html',
  // styleUrl: './app.component.css'
})



export class AppComponent implements OnInit {
  
  public respuesta: any ;
  public sData : ServerResponse[] = [];
  constructor(private http: HttpClient) {
    console.log('App component created');
  }

  ngOnInit(): void {
    this.fetchDetails();
    // console.log(this.sData[0].data)
  }

  public fetchDetails (){
    this.http.get('https://contaminados.akamai.meseguercr.com/api/games').subscribe(
      (response : any) => {
        this.sData = response;
        // console.log(this.sData);
      }
    );
  }

}
