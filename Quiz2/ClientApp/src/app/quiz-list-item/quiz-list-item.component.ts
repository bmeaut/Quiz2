import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Quiz } from '../quiz';
import { QuizService } from '../services/quiz.service';

@Component({
  selector: 'app-quiz-list-item',
  templateUrl: './quiz-list-item.component.html',
  styleUrls: ['./quiz-list-item.component.css']
})
export class QuizListItemComponent implements OnInit {

  @Input() quiz: Quiz;
  @Output() deleteQuizListItem: EventEmitter<any> = new EventEmitter();

  constructor(private router: Router, private quizService: QuizService) { }

  ngOnInit() {
  }

  showQuestions(quiz: Quiz): void {
    this.router.navigate(["/quizzes", this.quiz.id, "questions"]);
  }

  modifyQuiz(quiz: Quiz): void {
    this.router.navigate(["/quizzes", this.quiz.id, "edit"]);
  }

  deleteItem(quiz: Quiz): void {
    this.deleteQuizListItem.emit(quiz);
  }
}
