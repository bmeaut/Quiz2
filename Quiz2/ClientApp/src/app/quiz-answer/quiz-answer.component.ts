import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Answer } from '../answer';
import {GamesService} from "../services/games-service";

@Component({
  selector: 'app-quiz-answer',
  templateUrl: './quiz-answer.component.html',
  styleUrls: ['./quiz-answer.component.css']
})
export class QuizAnswerComponent implements OnInit {

  @Input() answer: Answer;
  @Input() timeIsOver: boolean;
  @Input() disableAnswer: boolean;
  @Output() answerHasBeenSelected = new EventEmitter<boolean>();
  constructor(public gameService: GamesService) { }

  ngOnInit() {
    console.debug("QuizQuestionComponent ngOnInit "+ this.gameService.getJoinId())
  }

  answerSelected(): void {
    this.gameService.sendAnswer(this.answer.id)
    this.answerHasBeenSelected.emit(true);
  }

}
