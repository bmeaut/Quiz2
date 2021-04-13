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

  quiz: Quiz = {
    id: 0,
    name: "",
    questions: [],
    owner: { id: 0 },
    games: [],
  };

  constructor(private quizSerivce: QuizService, private router: Router) { }

  ngOnInit() {
  }

  onSubmit(): void {
    console.log(this.quiz);
    this.quizSerivce.putQuiz(this.quiz).subscribe(quiz => {
      this.router.navigate(['/quizzes']);
    });
  }

}
