import { Component, OnInit } from '@angular/core';
import { Quiz } from '../../quiz';
import { QuizService } from '../../services/quiz.service';
import { AuthorizeService } from '../../../api-authorization/authorize.service';

@Component({
  selector: 'app-quiz-list',
  templateUrl: './quiz-list.component.html',
  styleUrls: ['./quiz-list.component.css']
})
export class QuizListComponent implements OnInit {

  quizzes: Quiz[];
  quizzesLoaded: boolean;

  constructor(private quizSerivce: QuizService, private authService: AuthorizeService) { }

  ngOnInit() {
    this.quizzes = [];
    this.getQuizList();
    this.quizzesLoaded = false;
  }

  getQuizList(): void {
    this.quizSerivce.getQuizzes().subscribe(quizzes => {
      this.quizzes = quizzes;
      this.quizzesLoaded = true;
    });
  }

  deleteQuiz(quiz: Quiz): void {
    this.quizSerivce.deleteQuiz(quiz.id).subscribe(() => {
      this.getQuizList();
    });
  }

}
