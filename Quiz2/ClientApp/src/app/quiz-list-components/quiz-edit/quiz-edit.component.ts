import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Quiz } from '../../quiz';
import { QuizService } from '../../services/quiz.service';

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
    owner: { id: ""},
    games: []
  };

  constructor(private router: Router, private quizService: QuizService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.getModifiedQuiz();
  }

  getModifiedQuiz(): void {
    let id = +this.route.snapshot.paramMap.get('id');
    this.quizService.getQuiz(id).subscribe(quiz => {
      this.quiz = quiz;
    });
  }

  modifyQuiz(): void {
    this.quizService.editQuiz(this.quiz).subscribe(quiz => {
      this.router.navigate(['/quizzes']);
    });
  }

}
