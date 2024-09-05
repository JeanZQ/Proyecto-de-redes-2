import { Component, ChangeDetectionStrategy } from "@angular/core";
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';

@Component({
    selector: 'userCard',
    imports: [MatButtonModule, MatCardModule, MatIconModule],
    standalone: true,
    templateUrl: './userCard.component.html',
    styleUrl: './userCard.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,

})

export class UserCardComponent {
    
}



