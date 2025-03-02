import { Component, OnInit } from '@angular/core';
import { QuizService } from '../../service/quiz.service';
import { Quiz, QuizStatus } from '../customize-quiz/customize-model/customize-quiz.model';
import { ResultService } from '../../service/result.service';
import { map } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';

interface CardSettings {
  title: string;
  iconClass: string;
  type: string;
  number: number;
}

@Component({
  selector: 'ngx-quiz-dash-board',
  templateUrl: './quiz-dash-board.component.html',
  styleUrls: ['./quiz-dash-board.component.scss']
})
export class QuizDashBoardComponent implements OnInit {
  dataChart: { value: number, name: string }[] = 
  [
    { value: 0, name: 'Score: 1' },
    { value: 0, name: 'Score: 2' },
    { value: 0, name: 'Score: 3' },
    { value: 0, name: 'Score: 4' },
    { value: 0, name: 'Score: 5' },
    { value: 0, name: 'Score: 6' },
    { value: 0, name: 'Score: 7' },
    { value: 0, name: 'Score: 8' },
    { value: 0, name: 'Score: 9' },
    { value: 0, name: 'Score: 10' },
  ];

  totalResults = 0;
  quizId = '';
  quiz?: Quiz;
  constructor(
    private quizService: QuizService,
    private resultService: ResultService,
    private router: ActivatedRoute,
  ) { }
  lightCards: CardSettings[] = [
    {
      title: 'Total Quizzes',
      iconClass: 'nb-lightbulb',
      type: 'success',
      number: 0
    },
    {
      title: 'Active Quizzes',
      iconClass: 'nb-lightbulb',
      type: 'warning',
      number: 0
    },
    {
      title: 'Total Results',
      iconClass: 'nb-lightbulb',
      type: 'danger',
      number: 0
    }
  ]

  ngOnInit(): void {
    this.quizService.getAll().subscribe((data) => {
      const totalQuizzes = data.length;
      const activeQuizzes = data.filter(quiz => quiz.status === QuizStatus.Ready).length;
      this.lightCards = [
        {
          title: 'Total Quizzes',
          iconClass: 'nb-lightbulb',
          type: 'success',
          number: totalQuizzes
        },
        {
          title: 'Active Quizzes',
          iconClass: 'nb-lightbulb',
          type: 'warning',
          number: activeQuizzes
        },
        {
          title: 'Total Results',
          iconClass: 'nb-lightbulb',
          type: 'danger',
          number: this.totalResults,
        }
      ]
    });

    this.router.queryParamMap.subscribe(params => {
      this.quizId = params?.get('id') ?? '' as string;
      this.innitChart();
    });
  }

  private innitChart() {
    if (this.quizId) {
      this.quizService.getById(this.quizId).subscribe((quiz) => {
        this.quiz = quiz;
      });

      this.resultService.getByQuizId(this.quizId).pipe(
        map(result => result ?? [])
      ).subscribe((results) => {
        this.setupChart(results);
      });
      return;
    }
    this.resultService.getAll().pipe(
      map(result => result ?? [])
    ).subscribe((results) => {
      this.setupChart(results);
    });
  }

  private setupChart(results: import("c:/Users/Lenovo/source/repos/MultipleChoise/multiplechoise.client/src/app/pages/quiz/model/exame.model").Result[]) {
    this.totalResults = results.length ?? 0;
    this.dataChart = this.dataChart.map(c => {
      switch (c.name) {
        case 'Score: 1':
          c.value = results.filter(r => r.score < 1.5).length;
          break;
        case 'Score: 2':
          c.value = results.filter(r => r.score >= 1.5 && r.score < 2.5).length;
          break;
        case 'Score: 3':
          c.value = results.filter(r => r.score >= 2.5 && r.score < 3.5).length;
          break;
        case 'Score: 4':
          c.value = results.filter(r => r.score >= 3.5 && r.score < 4.5).length;
          break;
        case 'Score: 5':
          c.value = results.filter(r => r.score >= 4.5 && r.score < 5.5).length;
          break;
        case 'Score: 6':
          c.value = results.filter(r => r.score >= 5.5 && r.score < 6.5).length;
          break;
        case 'Score: 7':
          c.value = results.filter(r => r.score >= 6.5 && r.score < 7.5).length;
          break;
        case 'Score: 8':
          c.value = results.filter(r => r.score >= 7.5 && r.score < 8.5).length;
          break;
        case 'Score: 9':
          c.value = results.filter(r => r.score >= 8.5 && r.score < 9.5).length;
          break;
        case 'Score: 10':
          c.value = results.filter(r => r.score >= 9.5).length;
          break;
      }
      return c;
    });
  }
}
