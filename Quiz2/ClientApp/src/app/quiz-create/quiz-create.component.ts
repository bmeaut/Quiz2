import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Quiz } from '../quiz';
import { QuizService } from '../services/quiz.service';
import { QuizUserService } from '../services/user.service';

@Component({
  selector: 'app-quiz-create',
  templateUrl: './quiz-create.component.html',
  styleUrls: ['./quiz-create.component.css']
})
export class QuizCreateComponent implements OnInit {

  quiz: Quiz;

  constructor(private quizSerivce: QuizService, private router: Router, private userService: QuizUserService) { }

  ngOnInit() {
    this.quiz = {
      id: 1,
      name: "",
      questions: [],
      owner: { id: "" },
      games: []
    };
    this.getUserID();
    console.log(this.quiz.owner.id);
  }

  getUserID(): void {
    this.userService.getUser().subscribe(user => {
      console.log(user);
      this.quiz.owner.id = user[0].id;
      console.log(this.quiz.owner.id);
    });
  }

  onSubmit(): void {
    this.quizSerivce.putQuiz(this.quiz).subscribe(quiz => {
      this.router.navigate(['/quizzes']);
    });
  }

}
