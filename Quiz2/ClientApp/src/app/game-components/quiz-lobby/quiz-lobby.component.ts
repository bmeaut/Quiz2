import { Component, OnInit } from '@angular/core';
import { User } from '../../user';
import {GamesService} from "../../services/games-service";

@Component({
  selector: 'app-quiz-lobby',
  templateUrl: './quiz-lobby.component.html',
  styleUrls: ['./quiz-lobby.component.css']
})
export class QuizLobbyComponent implements OnInit {

  users: User[] = [];

  constructor(public gameService: GamesService) { }

  ngOnInit() {
    this.gameService.newPlayer.subscribe( (players :User[]) => {
      this.users = players;
    });
  }

}
