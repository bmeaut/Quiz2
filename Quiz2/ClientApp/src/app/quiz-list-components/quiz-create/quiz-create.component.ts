import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { stringify } from 'querystring';
import { AuthorizeService } from '../../../api-authorization/authorize.service';
import { Quiz } from '../../quiz';
import { QuizService } from '../../services/quiz.service';

@Component({
  selector: 'app-quiz-create',
  templateUrl: './quiz-create.component.html',
  styleUrls: ['../shared-create-and-edit-style.css']
})
export class QuizCreateComponent implements OnInit {

  quizForm: FormGroup;
  quiz: Quiz = {
    id: 0,
    name: "",
    questions: null,
    owner: null,
    games: null,
  };

  constructor(private quizSerivce: QuizService, private router: Router) {}

  ngOnInit() {
    this.quizForm = new FormGroup({
      'quiznev': new FormControl(null, [Validators.required, Validators.minLength(3), Validators.maxLength(100)])
    });
  }

  createQuiz(): void {
    this.quiz.name = this.quizForm.get("quiznev").value;
    this.quizSerivce.putQuiz(this.quiz).subscribe(quiz => {
      this.router.navigate(['/quizzes']);
    });
  }

}
