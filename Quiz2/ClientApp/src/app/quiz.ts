import { Game } from "./game";
import { Question } from "./question";
import { User } from "./user";

export class Quiz {
    id: number;
    name: string;
    questions: Question[];
    owner: User;
    games: Game[];
}