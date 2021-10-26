import { Component, OnInit } from "@angular/core";
import { Game } from "src/app/game";
import { StatService} from "src/app/services/stat-service"
import { AuthorizeService } from "src/api-authorization/authorize.service";

@Component({
    selector: 'app-stat-list',
    templateUrl: './stat-list.component.html',
    styleUrls: ['./stat-list.component.css']
})
export class StatListComponent implements OnInit{
    games: Game[];
    gamesLoaded:boolean;

    constructor(private statService: StatService, private authService: AuthorizeService){}
    ngOnInit(){
        this.games = [];
        this.getOwnGameList();
        this.getPlayedGameList();
        this.gamesLoaded = false;
    }

    getOwnGameList():void{
        this.statService.getOwnGames().subscribe(games => {
            this.games = games;
            this.gamesLoaded = true;
        });
    }
    getPlayedGameList():void{
        this.statService.getPlayedGames().subscribe(games => {
            this.games = games;
            this.gamesLoaded = true;
        });
    }
}
