import { Game } from "./game";
import { Question } from "./question";
import { User } from "./user";

export class Quiz {
    id: number;
    name: string;
    users: User[];
    games: Game[];
    questions: Question[];
}