import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Question } from '../question';

@Component({
  selector: 'app-quiz-question-list-item',
  templateUrl: './quiz-question-list-item.component.html',
  styleUrls: ['./quiz-question-list-item.component.css']
})
export class QuizQuestionListItemComponent implements OnInit {

  @Input() question: Question;
  @Output() deleteQuestionListItem: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  deleteItem(question: Question): void {
    this.deleteQuestionListItem.emit(question);
  }

}
