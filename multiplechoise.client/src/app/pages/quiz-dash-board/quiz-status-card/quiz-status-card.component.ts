import { Component, Input } from "@angular/core";

@Component({
  selector: 'ngx-quiz-status-card',
  templateUrl: './quiz-status-card.component.html',
  styleUrls: ['./quiz-status-card.component.scss']
})
export class QuizStatusCardComponent {
  @Input() title: string;
  @Input() type: string;
  @Input()  number: number = 0;
}
