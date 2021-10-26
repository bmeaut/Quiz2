import { Game } from "./game";

export class ApplicationUser {
    id: string;
    name: string;
    ownGames: Game[];
    playedGames: Game[];
}
