import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgGridModule } from 'ag-grid-angular';
import { QuizManagementComponent } from './quiz-management.component';
import { NbButtonModule, NbCardModule, NbCheckboxModule, NbContextMenuModule, NbIconModule, NbInputModule, NbRadioModule, NbSelectModule } from '@nebular/theme';
import { ThemeModule } from '../../@theme/theme.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [QuizManagementComponent],
  imports: [
    CommonModule,
    AgGridModule,
    NbCardModule,
    ThemeModule,
    NbButtonModule,
    NbIconModule,
    NbSelectModule,
    NbInputModule,
    FormsModule,
    NbCheckboxModule,
    NbRadioModule,
    NbContextMenuModule,
  ]
})
export class QuizManagementModule { }
