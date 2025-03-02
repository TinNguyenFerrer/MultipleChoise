import { v4 } from "uuid";

export interface Answer extends BaseEntity, IdTracking {
  content: string;
  isCorrect: boolean;
  isSelected: boolean; // fe only
}

export interface Question extends BaseEntity, IdTracking {
  content: string;
  answers: Answer[];
}

export enum QuizStatus {
  Ready = 0,
  Pending,
  Published,
  Closed
}
export interface Quiz extends BaseEntity, IdTracking {
  title: string;
  status?: QuizStatus;
  durationInMinutes: number;
  questions: Question[];
  uniqueNumber: number;
}

export interface IdTracking {
  idTracking?: string;
}

export interface BaseEntity {
  id?: number;
  createdOn?: Date;
  modifiedOn?: Date;
}

export function createAnswer(answer: Partial<Answer> | null | undefined): Answer {
  if (!answer) {
    return {} as Answer;
  }

  return {
    idTracking: answer.idTracking ?? v4(),
    id: answer.id,
    isSelected: answer.isSelected ?? false,
    content: answer.content ?? '',
    isCorrect: answer.isCorrect?? false,
    createdOn: answer.createdOn,
    modifiedOn: answer.modifiedOn,
  };
}

export function createQuestion(question: Partial<Question>): Question {
  if (!question) {
    return {} as Question;
  }

  return {
    idTracking: question.idTracking?? v4(),
    id: question.id,
    content: question.content ?? '',
    answers: question.answers?.map(createAnswer) ?? [],
    createdOn: question.createdOn,
    modifiedOn: question.modifiedOn,
  };
}

export function createQuiz(quiz: Partial<Quiz>): Quiz {
  if (!quiz) {
    return {} as Quiz;
  }

  return {
    idTracking: quiz.idTracking?? v4(),
    status: quiz.status ?? QuizStatus.Pending,
    id: quiz.id,
    title: quiz.title,
    durationInMinutes: quiz.durationInMinutes ?? 0,
    questions: quiz.questions?.map(createQuestion) ?? [],
    createdOn: quiz.createdOn,
    modifiedOn: quiz.modifiedOn,
    uniqueNumber: quiz.uniqueNumber ?? 0,
  };
}

