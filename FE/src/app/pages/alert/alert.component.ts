import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
    selector: 'alert',
    templateUrl: './alert.component.html',
    styleUrls: ['./alert.component.css'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class AlertComponent {
    @Input() title!: string;
    @Input() message!: string;
    @Output() closeAlert = new EventEmitter<void>();

    onLeave(): void {
        this.closeAlert.emit();
    }
}