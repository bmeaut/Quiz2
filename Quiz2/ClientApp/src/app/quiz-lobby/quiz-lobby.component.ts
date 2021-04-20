import { Component, OnInit } from '@angular/core';
import { User } from '../user';

@Component({
  selector: 'app-quiz-lobby',
  templateUrl: './quiz-lobby.component.html',
  styleUrls: ['./quiz-lobby.component.css']
})
export class QuizLobbyComponent implements OnInit {

  users: User[] = [{id: "1"}, {id: "2"}, {id: "3"}, {id: "4"}];

  constructor() { }

  ngOnInit() {
  }

}
