import { Component, ChangeDetectionStrategy, Input, inject, model, signal, ChangeDetectorRef } from "@angular/core";
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIcon } from "@angular/material/icon";
import { GameResponse } from "../../models/app.interface";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MAT_DIALOG_DATA, MatDialog, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { FormsModule } from "@angular/forms";
import { DataService } from "../../services/data.service";
import { AlertComponent } from "../alert/alert.component";
import { CommonModule } from '@angular/common';

export interface DialogData {
    player: string;
    password: string;
}

@Component({
    selector: 'userRoom',
    imports: [
        CommonModule,
        MatButtonModule,
        MatCardModule,
        MatIcon,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        AlertComponent],
    standalone: true,
    templateUrl: './userRoom.component.html',
    styleUrl: './userRoom.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
})

export class UserRoomComponent {
    @Input() game!: GameResponse;

    showError = false;
    errorTitle = '';
    errorMessage = '';

    readonly player = signal('');
    readonly password = signal('');
    readonly dialog = inject(MatDialog);
    private dataService = inject(DataService);

    constructor(private cdr: ChangeDetectorRef) { }

    openPopUp(gameId: string): void {
        const dialogRef = this.dialog.open(PopUpComponent,
            {
                data: { player: this.player(), password: this.password() }
            }
        );
        dialogRef.afterClosed().subscribe(result => {
            if (result !== undefined) {
                this.player.set(result.player);
                this.password.set(result.password);

                const payload = {
                    id: gameId,
                    player: result.player(),
                    owner: result.player(),
                    password: result.password()
                };
                this.dataService.joinGame(payload).subscribe({
                    next: (response: any) => {
                        localStorage.setItem('GameResponse', JSON.stringify(response.data));
                        window.location.href = '/lobby';
                    },
                    error: (error: any) => {
                        this.showError = true;
                        this.errorMessage = "Could not enter the game.\nPlease try again later.";
                        this.errorTitle = "Error " + error.status;
                        this.cdr.detectChanges();
                    }
                });
            }
        });
    }

    onCloseAlert() {
        this.showError = false;
    }
}

@Component({
    selector: 'popUp',
    templateUrl: './popUpName.component.html',
    styleUrls: ['./popUpName.component.css'],
    standalone: true,
    imports: [
        MatFormFieldModule,
        MatInputModule,
        FormsModule,
        MatButtonModule,
        MatDialogTitle,
        MatDialogContent,
        MatDialogActions,
        MatDialogClose,
        AlertComponent
    ],
})

export class PopUpComponent {
    readonly dialogRef = inject(MatDialogRef<PopUpComponent>);
    readonly data = inject<DialogData>(MAT_DIALOG_DATA);
    readonly player = model(this.data.player);
    readonly password = model(this.data.password);

    onNoClick(): void {
        this.dialogRef.close();
    }
}
