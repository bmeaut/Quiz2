import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Quiz } from '../../quiz';
import { QuizService } from '../../services/quiz.service';
import {GamesService} from "../../services/games-service";

@Component({
  selector: 'app-stat-list-item',
  templateUrl: './stat-list-item.component.html',
  styleUrls: ['./stat-list-item.component.css'],
})
export class QuizListItemComponent implements OnInit {

  @Input() quiz: Quiz;
  @Output() deleteQuizListItem: EventEmitter<any> = new EventEmitter();

  constructor(private router: Router, private quizService: QuizService, private gameService: GamesService) { }

  ngOnInit() {
  }

  startQuiz(): void {
    this.gameService.createGame(this.quiz.id)
    this.router.navigate(["/game"]);
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
