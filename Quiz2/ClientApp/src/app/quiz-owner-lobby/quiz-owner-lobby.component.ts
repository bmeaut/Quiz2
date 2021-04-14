import { Component, OnInit } from '@angular/core';
import { User } from '../user';
import {GamesService} from "../services/games-service";

@Component({
  selector: 'app-quiz-owner-lobby',
  templateUrl: './quiz-owner-lobby.component.html',
  styleUrls: ['./quiz-owner-lobby.component.css'],
  providers:  [ GamesService ]
})
export class QuizOwnerLobbyComponent implements OnInit {

  users: User[] = [{id: 1}, {id: 2}, {id: 3}, {id: 4}];

  constructor(public gameService: GamesService) { }

  ngOnInit() {
  }

  startGame() {

  }

}
