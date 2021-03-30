import { Component, Input, OnInit } from '@angular/core';
import { Answer } from '../answer';

@Component({
  selector: 'app-quiz-answer',
  templateUrl: './quiz-answer.component.html',
  styleUrls: ['./quiz-answer.component.css']
})
export class QuizAnswerComponent implements OnInit {

  @Input() answer: Answer;
  constructor() { }

  ngOnInit() {
  }

}
