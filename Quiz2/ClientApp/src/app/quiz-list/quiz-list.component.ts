import { Component, OnInit } from '@angular/core';
import { Quiz } from '../quiz';
import { QuizService } from '../services/quiz.service';

@Component({
  selector: 'app-quiz-list',
  templateUrl: './quiz-list.component.html',
  styleUrls: ['./quiz-list.component.css']
})
export class QuizListComponent implements OnInit {

  quizzes: Quiz[];

  constructor(private quizSerivce: QuizService) { }

  ngOnInit() {
    this.getQuizList();
  }

  getQuizList(): void {
    this.quizSerivce.getQuizzes().subscribe(quizzes => {
      this.quizzes = quizzes;
    });
  }

  deleteQuiz(quiz: Quiz): void {
    this.quizSerivce.deleteQuiz(quiz.id).subscribe(() => {
      this.getQuizList();
    });
  }

}
