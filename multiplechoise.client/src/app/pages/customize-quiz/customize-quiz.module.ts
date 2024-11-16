import { NgModule } from '@angular/core';
import {
  NbButtonModule,
  NbCardModule, NbIconModule,
  NbSelectModule, NbInputModule,
  NbCheckboxModule,
  NbRadioModule
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
  ],
  declarations: [
    CustomizeQuizComponent
  ],
})
export class CustomizeQuizModule { }
