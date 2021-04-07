import { Component, OnInit } from '@angular/core';
import { User } from '../user';

@Component({
  selector: 'app-quiz-owner-lobby',
  templateUrl: './quiz-owner-lobby.component.html',
  styleUrls: ['./quiz-owner-lobby.component.css']
})
export class QuizOwnerLobbyComponent implements OnInit {

  users: User[] = [{id: 1}, {id: 2}, {id: 3}, {id: 4}];

  constructor() { }

  ngOnInit() {
  }

  startGame() {
    
  }

}
