import { Answer } from "./answer";

export class Question {
    id: number;
    quizId: number;
    text: string;
    timer: number;
    position: number;
    points: number;
    numberOfCorrectAnswers: number;
    answers: Answer[];
}