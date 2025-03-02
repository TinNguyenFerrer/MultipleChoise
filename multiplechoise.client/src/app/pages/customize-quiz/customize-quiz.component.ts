import { AfterViewChecked, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Answer, createAnswer, createQuestion, createQuiz, Question, Quiz } from './customize-model/customize-quiz.model';
import { NbToastrService } from '@nebular/theme';
import { v4 } from 'uuid';
import { QuizService } from '../../service/quiz.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'ngx-customize-quiz',
  templateUrl: './customize-quiz.component.html',
  styleUrls: ['./customize-quiz.component.scss']
})
export class CustomizeQuizComponent implements AfterViewChecked, OnInit {
  isEditing: boolean = false;
  quizId: string = null;

  quiz: Quiz = {
    idTracking: v4(),
    uniqueNumber: 0,
    title: "Untitled Quiz",
    durationInMinutes: 30,
    questions: [
      this.createDefaultEmptyQuestion()
    ],
  };

  @ViewChild('inputField') inputField: ElementRef | undefined;

  constructor(
    private toastrService: NbToastrService,
    private quizService: QuizService,
    private router: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.router.queryParamMap.subscribe(params => {
      this.quizId = params.get('id') ?? '' as string;
      if (this.quizId) {
        this.quizService.getById(this.quizId).subscribe(data => {
          this.quiz = createQuiz(data);
        })
      }
    });
  }

  ngAfterViewChecked() {
    if (this.isEditing && this.inputField) {
      this.inputField.nativeElement.focus();
    }
  }

  onIsCorrectChecked(question: Question, rightAnswer: Answer): void {
    question.answers.forEach(answer => {
      if (answer.idTracking && answer.idTracking === rightAnswer.idTracking) {
        answer.isCorrect = true;
      } else {
        answer.isCorrect = false;
      }
    });
  }

  onAddAnswer(question: Question) {
    question.answers.push(createAnswer({}));
  }

  onAddQuestion() {
    this.quiz.questions.push(this.createDefaultEmptyQuestion());
  }

  private createDefaultEmptyQuestion(): Question {
    return createQuestion(
      {
        answers: [createAnswer({}), createAnswer({}), createAnswer({}), createAnswer({})]
      }
    );
  }

  onRemoveAnswer(question: Question, answer: Answer, ) {
    const index = question.answers.indexOf(answer);
    if (index > -1) {
      question.answers.splice(index, 1);
    }
  }

  onRemoveQuestion(question: Question) {
    const index = this.quiz.questions.indexOf(question);
    if (index > -1) {
      this.quiz.questions.splice(index, 1);
    }
  }

  onSaveQuiz() {
    if (this.quiz.id) {
      this.quizService.update(this.quiz).subscribe(
        data => {
          this.quiz = createQuiz(data);
          this.toastrService.success('Quiz saved successfully!', 'Notification');
        });
      return;
    }

    this.quizService.create(this.quiz).subscribe(
      data => {
        this.quiz = createQuiz(data);
        this.toastrService.success('Quiz created successfully!', 'Notification');
      });
  }
}
