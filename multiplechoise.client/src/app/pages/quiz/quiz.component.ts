import { Component, ElementRef, OnDestroy, OnInit, QueryList, ViewChildren } from '@angular/core';
import { interval, Subscription } from 'rxjs';
import { createParticipantInformation, createResult, isParticipantInformationValid, ParticipantInformation } from './model/exame.model';
import { QuizService } from '../../service/quiz.service';
import { Answer, createQuiz, Question, Quiz } from '../customize-quiz/customize-model/customize-quiz.model';
import { ResultService } from '../../service/result.service';

@Component({
  selector: 'ngx-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.scss']
})
export class QuizComponent implements OnInit, OnDestroy {
  isParticipantInformationReady: boolean = false;
  @ViewChildren('questionItem') questionItems!: QueryList<ElementRef>;
  quiz: Quiz = {} as Quiz;

  constructor(
    private readonly quizService: QuizService,
    private readonly resultService: ResultService,
  ) { }

  currentTime?: number;
  timerSubscription!: Subscription;
  emptyArray = Array.from({ length: 12 });
  resultInfor: { correct: number, total: number, score: number } | null = null;
  participantInformation?: ParticipantInformation;
  ngOnInit() {
    this.participantInformation = createParticipantInformation({});
  }

  ngOnDestroy() {
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
  }

  get isShowParticipantInformation() {
    return !this.isParticipantInformationReady;
  }

  get isParticipantInformationValid() {
    return isParticipantInformationValid(this.participantInformation);
  }

  get isStartQuiz() {
    return this.isParticipantInformationReady
      && !this.resultInfor;
  }

  get isShowResult() {
    return !!this.resultInfor
     && this.isParticipantInformationReady
  }

  isAnswered(question: Question) {
    return question?.answers?.some(a => a.isSelected)
  }

  scrollToQuestion(index: number) {
    const elements = this.questionItems.toArray();
    if (elements[index]) {
      elements[index].nativeElement.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
  }

  selectOption(question: Question, answer: Answer, isChecked: boolean) {
    console.log(question, answer, isChecked);
    
    question.answers.forEach(a => {
      if (a.idTracking === answer.idTracking) {
        a.isSelected = true;
      } else {
        a.isSelected = false;
      }
    });
  }

  selectedAnswer(question: Question) {
    return question.answers.find(answer => answer.isSelected);
  }

  formatTime(seconds: number): string {
    const minutes = Math.floor(seconds / 60) || 0;
    const remainingSeconds = seconds % 60 || 0;
    return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
  }

  startTimer() {
    this.currentTime = this.quiz.durationInMinutes * 60;
    this.timerSubscription = interval(1000).subscribe(() => {
      if (this.currentTime > 0) {
        this.currentTime--;
      } else {
        this.submitQuiz();
      }
    });
  }

  startExame() {    
    if (!this.isParticipantInformationValid) {
      return;
    }

    this.quizService.getByNumber(this.participantInformation?.quizId).subscribe(q => {
      this.quiz = createQuiz(q);
      this.isParticipantInformationReady = true;
      this.startTimer();
      this.resultInfor = null;
    }
  );
  }

  submitQuiz() {
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }

    const requestResult = createResult({
      studentName: this.participantInformation.fullName,
      studentId: this.participantInformation.participantId,
      quiz: this.quiz,
    });


    this.resultService.getResutlByQuiz(requestResult).subscribe(result => {      
      this.resultInfor = { correct: result.correctAnswers, total: this.quiz.questions.length, score: result.score };
    },
  () => {

  });
  }
}
