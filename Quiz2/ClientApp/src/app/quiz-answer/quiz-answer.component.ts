import { Component, Input, OnInit, Output } from '@angular/core';
import { Answer } from '../answer';
import {GamesService} from "../services/games-service";

@Component({
  selector: 'app-quiz-answer',
  templateUrl: './quiz-answer.component.html',
  styleUrls: ['./quiz-answer.component.css']
})
export class QuizAnswerComponent implements OnInit {

  @Input() answer: Answer;
  constructor(public gameService: GamesService) { }

  ngOnInit() {
  }

}
