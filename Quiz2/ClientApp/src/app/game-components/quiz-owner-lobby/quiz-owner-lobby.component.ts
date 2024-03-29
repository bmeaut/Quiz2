import { Component, OnInit } from '@angular/core';
import { User } from '../../user';
import {GamesService} from "../../services/games-service";

@Component({
  selector: 'app-quiz-owner-lobby',
  templateUrl: './quiz-owner-lobby.component.html',
  styleUrls: ['./quiz-owner-lobby.component.css'],
})
export class QuizOwnerLobbyComponent implements OnInit {

  users: User[] = [];

  constructor(public gameService: GamesService) { }

  ngOnInit() {
    this.gameService.newPlayer.subscribe( (players :User[]) => {
      this.users = players;
    });
  }
}
