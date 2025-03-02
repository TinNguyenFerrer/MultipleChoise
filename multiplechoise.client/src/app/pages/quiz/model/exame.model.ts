import { Quiz } from "../../customize-quiz/customize-model/customize-quiz.model";

export interface ParticipantInformation {
  fullName: string;
  participantId: string;
  quizId: string;
}
export interface Result {
  studentName: string; 
  studentId: string;
  correctAnswers: number;
  score: number;
  quiz: Quiz;
}

export function createResult(params: Partial<Result> | null | undefined) {
  if (!params) {
    return {} as Result;
  }
  return {
    studentName: params.studentName,
    studentId: params.studentId,
    correctAnswers: params.correctAnswers,
    score: params.score,
    quiz: params.quiz,
  };
}


export function  createParticipantInformation(params: Partial<ParticipantInformation> | null | undefined) {
  
  if (!params) {
    return {} as ParticipantInformation;
  }
  return {
    fullName: params.fullName,
    participantId: params.participantId,
    quizId: params.quizId,
  };
}

export function isParticipantInformationValid(participantInformation: ParticipantInformation) {
  return !!participantInformation.fullName
    && !!participantInformation.participantId
    && !!participantInformation.quizId;
}
