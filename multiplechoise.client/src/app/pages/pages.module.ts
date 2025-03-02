import { NgModule } from '@angular/core';
import { NbFormFieldModule, NbMenuModule } from '@nebular/theme';

import { ThemeModule } from '../@theme/theme.module';
import { PagesComponent } from './pages.component';
import { DashboardModule } from './dashboard/dashboard.module';
import { ECommerceModule } from './e-commerce/e-commerce.module';
import { PagesRoutingModule } from './pages-routing.module';
import { MiscellaneousModule } from './miscellaneous/miscellaneous.module';
import { CustomizeQuizModule } from './customize-quiz/customize-quiz.module';
import { QuizManagementModule } from './quiz-management/quiz-management.module';
import { QuizModule } from './quiz/module/quiz/quiz.module';
import { QuizDashBoardModule } from './quiz-dash-board/quiz-dash-board/quiz-dash-board.module';

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    NbMenuModule,
    DashboardModule,
    ECommerceModule,
    MiscellaneousModule,
    CustomizeQuizModule,
    QuizManagementModule,
    QuizModule,
    QuizDashBoardModule,
    NbFormFieldModule,
  ],
  declarations: [
    PagesComponent,
  ],
})
export class PagesModule {
}
