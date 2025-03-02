import { Component } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { QuizStatus } from '../../../pages/customize-quiz/customize-model/customize-quiz.model';

@Component({
  selector: 'ngx-quiz-status-icon-renderer-component',
  templateUrl: './quiz-status-icon-renderer-component.component.html',
  styleUrls: ['./quiz-status-icon-renderer-component.component.scss']
})
export class QuizStatusIconRendererComponent implements ICellRendererAngularComp {
  status: QuizStatus = QuizStatus.Pending;
  QuizStatus = QuizStatus;
  agInit(params: any): void {
    console.log(params.value, QuizStatus.Ready);
    
    this.status = params.value;
  }

  refresh(): boolean {
    return false;
  }
}
