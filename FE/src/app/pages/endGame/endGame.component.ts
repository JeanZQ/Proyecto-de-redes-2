import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
    selector: 'end-game',
    templateUrl: './endGame.component.html',
    styleUrls: ['./endGame.component.css'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class EndGameComponent {
    @Input() title!: string;
    constructor() {
    }
}