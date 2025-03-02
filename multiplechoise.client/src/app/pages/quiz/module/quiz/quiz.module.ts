import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NbButtonModule, NbCardModule, NbCheckboxModule, NbIconModule, NbInputModule, NbLayoutModule, NbRadioModule, NbSelectModule, NbSidebarModule } from '@nebular/theme';
import { ThemeModule } from '../../../../@theme/theme.module';
import { FormsModule } from '@angular/forms';
import { QuizComponent } from '../../quiz.component';



@NgModule({
  declarations: [QuizComponent],
  imports: [
    CommonModule,
    CommonModule,
    NbCardModule,
    ThemeModule,
    NbButtonModule,
    NbIconModule,
    NbSelectModule,
    NbInputModule,
    FormsModule,
    NbCheckboxModule,
    NbRadioModule,
    NbLayoutModule,
    NbSidebarModule.forRoot(),
  ]
})
export class QuizModule { }
