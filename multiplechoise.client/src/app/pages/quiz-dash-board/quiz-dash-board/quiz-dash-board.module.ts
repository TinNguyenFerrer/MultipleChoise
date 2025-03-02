import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ThemeModule } from '../../../@theme/theme.module';
import { NbActionsModule, NbButtonModule, NbCardModule, NbCheckboxModule, NbFormFieldModule, NbIconModule, NbInputModule, NbListModule, NbRadioModule, NbSelectModule, NbTabsetModule, NbUserModule } from '@nebular/theme';
import { NgxEchartsModule } from 'ngx-echarts';
import { QuizDashBoardComponent } from '../quiz-dash-board.component';
import { DashboardModule } from '../../dashboard/dashboard.module';
import { QuizStatusCardComponent } from '../quiz-status-card/quiz-status-card.component';
import { QChartModule } from '../q-chartjs/q-chart.module';



@NgModule({
  imports: [
      FormsModule,
      ThemeModule,
      NbCardModule,
      NbUserModule,
      NbButtonModule,
      NbTabsetModule,
      NbActionsModule,
      NbRadioModule,
      NbSelectModule,
      NbListModule,
      NbIconModule,
      NbButtonModule,
      NgxEchartsModule,
      CommonModule,
      DashboardModule,
      ThemeModule,
      NbInputModule,
      NbCheckboxModule,
      NbFormFieldModule,
      QChartModule,
    ],
    declarations: [
      QuizDashBoardComponent,
      QuizStatusCardComponent,
    ],
})
export class QuizDashBoardModule { }
