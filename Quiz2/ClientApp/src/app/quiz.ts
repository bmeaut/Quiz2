import { Game } from "./game";
import { Question } from "./question";
import { User } from "./user";

export interface Quiz {
    id: number;
    name: string;
    questions: Question[];
    owner: User;
    games: Game[];
}