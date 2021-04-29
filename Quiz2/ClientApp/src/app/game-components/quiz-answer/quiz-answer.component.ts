import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Answer } from '../../answer';
import {GamesService} from "../../services/games-service";

@Component({
  selector: 'app-quiz-answer',
  templateUrl: './quiz-answer.component.html',
  styleUrls: ['./quiz-answer.component.css']
})
export class QuizAnswerComponent implements OnInit {

  @Input() answer: Answer;
  @Input() timeIsOver: boolean;
  @Input() disableAnswer: boolean;
  @Input() index: number;
  @Output() answerIsChecked = new EventEmitter<Answer>();

  letters: string[];

  constructor(public gameService: GamesService) {
  }

  ngOnInit() {
    console.debug("QuizQuestionComponent ngOnInit " + this.gameService.getJoinId())
    this.letters = ['A', 'B', 'C', 'D'];
  }

  answerSelected(): void {
    this.gameService.sendAnswer(this.answer.id)
    //this.answerHasBeenSelected.emit(true);

  }
  sendAnswerToParentComponent(isChecked: boolean): void {

    this.answer.correct = isChecked;
    this.answerIsChecked.emit(this.answer);
  }
}
