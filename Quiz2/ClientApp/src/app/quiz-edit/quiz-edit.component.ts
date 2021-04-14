import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Quiz } from '../quiz';
import { QuizService } from '../services/quiz.service';

@Component({
  selector: 'app-quiz-edit',
  templateUrl: './quiz-edit.component.html',
  styleUrls: ['./quiz-edit.component.css']
})
export class QuizEditComponent implements OnInit {

  quiz: Quiz;

  constructor(private router: Router, private quizService: QuizService) { }

  ngOnInit() {
    this.getModifiedQuiz();
  }

  getModifiedQuiz(): void {
    this.quiz = this.quizService.getModifiedQuiz();
  }

  modifyQuiz(): void {
    this.quizService.editQuiz(this.quiz).subscribe(quiz => {
      this.router.navigate(['/quizzes']);
    });
  }

}
