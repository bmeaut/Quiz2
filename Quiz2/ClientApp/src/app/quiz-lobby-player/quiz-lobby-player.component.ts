import { Component, OnInit, Input } from '@angular/core';
import { User } from '../user';

@Component({
  selector: 'app-quiz-lobby-player',
  templateUrl: './quiz-lobby-player.component.html',
  styleUrls: ['./quiz-lobby-player.component.css']
})
export class QuizLobbyPlayerComponent implements OnInit {

  @Input() user: User;

  constructor() { }

  ngOnInit() {
  }

}
