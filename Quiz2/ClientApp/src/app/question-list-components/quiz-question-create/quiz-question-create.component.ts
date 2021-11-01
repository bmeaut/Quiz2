import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Question } from '../../question';
import { QuizQuestionService } from '../../services/quizquestion.service';

@Component({
  selector: 'app-quiz-question-create',
  templateUrl: './quiz-question-create.component.html',
  styleUrls: ['../shared-create-and-edit-style.css']
})
export class QuizQuestionCreateComponent implements OnInit {

  questionForm: FormGroup;
  question: Question;

  constructor(
    private router: Router,
    private questionService: QuizQuestionService,
    private route: ActivatedRoute
    ) { }

  ngOnInit() {
    this.questionForm = new FormGroup({
      'questiontext': new FormControl(null, [Validators.required, Validators.minLength(5), Validators.maxLength(300)]),
      'score': new FormControl(null, [Validators.required, Validators.min(0), Validators.pattern("^[0-9]*$"),]),
      'timeToAnswer': new FormControl(null, [Validators.required, Validators.min(4), Validators.pattern("^[0-9]*$")]),
      'position': new FormControl(null, [Validators.required, Validators.min(1), Validators.pattern("^[0-9]*$")]),
      'answerA': new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(200)]),
      'answerB': new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(200)]),
      'answerC': new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(200)]),
      'answerD': new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(200)]),
      'answerACorrect': new FormControl(false),
      'answerBCorrect': new FormControl(false),
      'answerCCorrect': new FormControl(false),
      'answerDCorrect': new FormControl(false),
    });
  }

  createQuestion(): void {
    let id = this.route.snapshot.paramMap.get('id');
    this.question.text = this.questionForm.get('questiontext').value;
    this.question.points = this.questionForm.get('score').value;
    this.question.secondsToAnswer = this.questionForm.get('timeToAnswer').value;
    this.question.position = this.questionForm.get('position').value;
    this.question.answers[0].text = this.questionForm.get('answerA').value;
    this.question.answers[0].correct = this.questionForm.get('answerACorrect').value;
    this.question.answers[1].text = this.questionForm.get('answerB').value;
    this.question.answers[1].correct = this.questionForm.get('answerBCorrect').value;
    this.question.answers[2].text = this.questionForm.get('answerC').value;
    this.question.answers[2].correct = this.questionForm.get('answerCCorrect').value;
    this.question.answers[3].text = this.questionForm.get('answerD').value;
    this.question.answers[3].correct = this.questionForm.get('answerDCorrect').value;
    this.question.quizId = +id;
    this.questionService.putQuestion(this.question).subscribe(res => {
      this.router.navigate(['../'], { relativeTo: this.route });
    });
  }
}
