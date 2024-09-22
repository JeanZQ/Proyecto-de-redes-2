import { Component, ChangeDetectionStrategy, Input, OnInit, inject, model, signal } from "@angular/core";
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIcon } from "@angular/material/icon";
import { GameResponse } from "../../models/app.interface";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MAT_DIALOG_DATA, MatDialog, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { FormsModule } from "@angular/forms";
import { DataService } from "../../services/data.service";

export interface DialogData {
    player: string;
    password: string;
}

@Component({
    selector: 'userRoom',
    imports: [
        MatButtonModule,
        MatCardModule,
        MatIcon,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule],
    standalone: true,
    templateUrl: './userRoom.component.html',
    styleUrl: './userRoom.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,

})

export class UserRoomComponent {
    @Input() game!: GameResponse;
    readonly player = signal('');
    readonly password = signal('');
    readonly dialog = inject(MatDialog);
    private dataService = inject(DataService);

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
                        alert('Error al unirse al juego');
                    }
                });
            }
        });
    }
}

@Component({
    selector: 'popUp',
    templateUrl: './popUpName.component.html',
    styleUrl: './popUpName.component.css',
    standalone: true,
    imports: [
        MatFormFieldModule,
        MatInputModule,
        FormsModule,
        MatButtonModule,
        MatDialogTitle,
        MatDialogContent,
        MatDialogActions,
        MatDialogClose
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