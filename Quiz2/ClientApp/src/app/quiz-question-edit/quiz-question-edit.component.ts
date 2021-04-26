import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Question } from '../question';
import { QuizQuestionService } from '../services/quizquestion.service';

@Component({
  selector: 'app-quiz-question-edit',
  templateUrl: './quiz-question-edit.component.html',
  styleUrls: ['./quiz-question-edit.component.css']
})
export class QuizQuestionEditComponent implements OnInit {

  question: Question;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private questionService: QuizQuestionService
  ) { }

  ngOnInit() {
    this.getQuestion();
  }

  getQuestion(): void {
    let id = +this.route.snapshot.paramMap.get('questionid');
    this.questionService.getQuestion(id).subscribe(question => {
      this.question = question;
    });
  }

  onSubmit(): void {
    for(let answer of this.question.answers) {
      answer.questionId = this.question.id;
    }
    this.questionService.putQuestion(this.question).subscribe(() => {
      this.router.navigate(['../'], { relativeTo: this.route });
    });
  }

}
