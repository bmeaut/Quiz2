import { Component, Input, OnInit } from '@angular/core';
import { Answer } from '../answer';

@Component({
  selector: 'app-quiz-statistics-answer',
  templateUrl: './quiz-statistics-answer.component.html',
  styleUrls: ['./quiz-statistics-answer.component.css']
})
export class QuizStatisticsAnswerComponent implements OnInit {

  @Input() answer: Answer;

  constructor() { }

  ngOnInit() {
  }

}
