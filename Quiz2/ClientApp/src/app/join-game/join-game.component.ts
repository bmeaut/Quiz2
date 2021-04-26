import { Component, OnInit } from '@angular/core';
import {GamesService} from "../services/games-service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-join-game',
  templateUrl: './join-game.component.html',
  styleUrls: ['./join-game.component.css']
})
export class JoinGameComponent implements OnInit {

  joinId: string;

  constructor(private router: Router, private gameService: GamesService) { }

  ngOnInit() {
  }

  join(){
    this.gameService.joinGame(this.joinId);
    this.router.navigate(["/game"]);
  }
}
