import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NbButtonModule, NbCardModule, NbCheckboxModule, NbDialogRef, NbIconModule, NbInputModule, NbRadioModule, NbSelectModule } from '@nebular/theme';

@Component({
  selector: 'ngx-app-result-dialog',
  templateUrl: './app-result-dialog.component.html',
  styleUrls: ['./app-result-dialog.component.scss'],
  standalone: true,
  imports: [CommonModule, NbCardModule, NbButtonModule,
        NbIconModule,
        NbSelectModule,
        NbInputModule,
        FormsModule,
        NbCheckboxModule,
        NbRadioModule,
  ],
})
export class AppResultDialogComponent {
  @Input() results: any;

  constructor(private dialogRef: NbDialogRef<AppResultDialogComponent>) {}

  close() {
    this.dialogRef.close();
  }
}
