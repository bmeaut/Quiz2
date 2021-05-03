import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Quiz } from '../../quiz';
import { QuizService } from '../../services/quiz.service';

@Component({
  selector: 'app-quiz-edit',
  templateUrl: './quiz-edit.component.html',
  styleUrls: ['../shared-create-and-edit-style.css']
})
export class QuizEditComponent implements OnInit {

  quizForm: FormGroup;
  quiz: Quiz = {
    id: 0,
    name: "",
    questions: [],
    owner: { id: "", name: ""},
    games: []
  };

  constructor(private quizService: QuizService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.quizForm = new FormGroup({
      'quiznev': new FormControl(null, [Validators.required, Validators.minLength(3), Validators.maxLength(100)])
    });
    this.getModifiedQuiz();
  }

  getModifiedQuiz(): void {
    let id = +this.route.snapshot.paramMap.get('id');
    this.quizService.getQuiz(id).subscribe(quiz => {
      this.quiz = quiz;
      this.quizForm.get('quiznev').setValue(quiz.name);
    });
  }

  modifyQuiz(): void {
    this.quiz.name = this.quizForm.get('quiznev').value;
    this.quizService.editQuiz(this.quiz).subscribe(quiz => {
      this.router.navigate(['/quizzes']);
    });
  }

}
