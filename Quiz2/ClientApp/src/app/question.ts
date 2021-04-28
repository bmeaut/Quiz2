import { Answer } from "./answer";

export interface Question {
    id: number;
    quizId?: number;
    text: string;
    answers: Answer[];
    secondsToAnswer: number;
    position: number;
    points: number;
}
