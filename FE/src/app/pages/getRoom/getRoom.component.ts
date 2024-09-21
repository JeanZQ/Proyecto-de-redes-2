import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { getRoomInputComponent } from "./getRoomInput.component";

/**
 * @title Dialog with header, scrollable content and actions
 */
@Component({
  selector: 'getRoom',
  templateUrl: './getRoom.component.html',
  styleUrl: './getRoom.component.css',
  standalone: true,
  imports: [MatButtonModule, MatDialogModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class getRoomComponent {
  readonly dialog = inject(MatDialog);

  openDialog() {
    const dialogRef = this.dialog.open(getRoomPopUpComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
}

@Component({
  selector: 'getRoomPopUp',
  templateUrl: './getRoomPopUp.component.html',
  styleUrl: './getRoom.component.css',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, getRoomComponent, getRoomInputComponent],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class getRoomPopUpComponent {}
