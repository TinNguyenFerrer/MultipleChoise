import { NgModule } from '@angular/core';
import {
  NbButtonModule,
  NbCardModule, NbIconModule,
  NbSelectModule, NbInputModule,
  NbCheckboxModule,
  NbRadioModule,
  NbFormFieldModule
} from '@nebular/theme';

import { ThemeModule } from '../../@theme/theme.module';

import { CustomizeQuizComponent } from './customize-quiz.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  imports: [
    ThemeModule,
    NbCardModule,
    NbButtonModule,
    NbIconModule,
    NbSelectModule,
    NbInputModule,
    FormsModule,
    NbCheckboxModule,
    NbRadioModule,
    NbFormFieldModule,
  ],
  declarations: [
    CustomizeQuizComponent,
  ],
})
export class CustomizeQuizModule { }
