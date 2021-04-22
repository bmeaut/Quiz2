import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { stringify } from 'querystring';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { Quiz } from '../quiz';
import { QuizService } from '../services/quiz.service';

@Component({
  selector: 'app-quiz-create',
  templateUrl: './quiz-create.component.html',
  styleUrls: ['./quiz-create.component.css']
})
export class QuizCreateComponent implements OnInit {

  quiz: Quiz = {
    id: 1,
    name: "",
    questions: [],
    owner: { id: "" },
    games: []
  };;

  constructor(private quizSerivce: QuizService, private router: Router, private authService: AuthorizeService) { }

  ngOnInit() {
  }

  onSubmit(): void {
    this.quizSerivce.putQuiz(this.quiz).subscribe(quiz => {
      this.router.navigate(['/quizzes']);
    });
  }

}
