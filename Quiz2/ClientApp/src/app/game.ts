import { User } from "./user";

export interface Game {
    quizId: number;
    currentQuestionId: number;
    users: User[];
}