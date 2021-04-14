import { Answer } from "./answer";
import { Quiz } from "./quiz";

export class Question {
    id: number;
    quiz: Quiz;
    text: string;
    answers: Answer[];
    secondsToAnswer: number;
    position: number;
    points: number;
}
