import { Component } from '@angular/core';
import { Answer, createAnswer, createQuestion, Question, Quiz } from './customize-model/customize-quiz.model';
import { NbToastrService } from '@nebular/theme';

@Component({
  selector: 'ngx-customize-quiz',
  templateUrl: './customize-quiz.component.html',
  styleUrls: ['./customize-quiz.component.scss']
})
export class CustomizeQuizComponent {
  constructor(private toastrService: NbToastrService) {}
  
  quiz: Quiz = {
    id: 1,
    title: "Bài kiểm tra lập trình",
    description: "Kiểm tra kiến thức cơ bản về Angular",
    questions: [
      {
        id: 1,
        text: "Angular là gì?",
        answers: [
          { id: 1, text: "Framework", isCorrect: true },
          { id: 2, text: "Library", isCorrect: false },
          { id: 3, text: "Tool", isCorrect: false },
        ],
      },
    ],
  };

  onIsCorrectChecked(question: Question, rightAnswer: Answer): void {
    question.answers.forEach(answer => {
      if (answer.id === rightAnswer.id) {
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
    this.quiz.questions.push(createQuestion({ answers: [createAnswer({})]}));
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
    this.toastrService.success('Lưu bài kiểm tra thành công!', 'Thông báo');
  }
}
