import { Component, Input, OnInit } from '@angular/core';
import { Question } from '../question';

@Component({
  selector: 'app-quiz-question-list-item',
  templateUrl: './quiz-question-list-item.component.html',
  styleUrls: ['./quiz-question-list-item.component.css']
})
export class QuizQuestionListItemComponent implements OnInit {

  @Input() question: Question;
  constructor() { }

  ngOnInit() {
  }

}
