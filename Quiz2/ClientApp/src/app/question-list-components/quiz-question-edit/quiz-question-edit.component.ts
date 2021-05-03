import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Question } from '../../question';
import { QuizQuestionService } from '../../services/quizquestion.service';

@Component({
  selector: 'app-quiz-question-edit',
  templateUrl: './quiz-question-edit.component.html',
  styleUrls: ['./quiz-question-edit.component.css']
})
export class QuizQuestionEditComponent implements OnInit {

  questionForm: FormGroup;
  question: Question = {
    id: 0,
    quizId: 0,
    text: "",
    answers: [{id: 0, questionID: 0, correct: true, text: ""},
    {id: 0, questionID: 0 , correct: true, text: ""},
    {id: 0, questionID: 0, correct: true, text: ""},
    {id: 0, questionID: 0, correct: true, text: ""}
    ],
    secondsToAnswer: 0,
    position: 0,
    points: 0
  };

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private questionService: QuizQuestionService
  ) { }

  ngOnInit() {
    this.questionForm = new FormGroup({
      'questiontext': new FormControl(null, [Validators.required, Validators.minLength(5), Validators.maxLength(300)]),
      'score': new FormControl(null, [Validators.required, Validators.min(0), Validators.pattern("^[0-9]*$"),]),
      'timeToAnswer': new FormControl(null, [Validators.required, Validators.min(4), Validators.pattern("^[0-9]*$")]),
      'position': new FormControl(null, [Validators.required, Validators.min(1), Validators.pattern("^[0-9]*$")]),
      'answerA': new FormControl(null, [Validators.required, Validators.minLength(3), Validators.maxLength(200)]),
      'answerB': new FormControl(null, [Validators.required, Validators.minLength(3), Validators.maxLength(200)]),
      'answerC': new FormControl(null, [Validators.required, Validators.minLength(3), Validators.maxLength(200)]),
      'answerD': new FormControl(null, [Validators.required, Validators.minLength(3), Validators.maxLength(200)]),
      'answerACorrect': new FormControl(false),
      'answerBCorrect': new FormControl(false),
      'answerCCorrect': new FormControl(false),
      'answerDCorrect': new FormControl(false),
    });
    this.getQuestion();
  }

  getQuestion(): void {
    let id = +this.route.snapshot.paramMap.get('questionid');
    this.questionService.getQuestion(id).subscribe((question: Question) => {
      this.question = question;
      this.questionForm.get('questiontext').setValue(question.text);
      this.questionForm.get('score').setValue(question.points);
      this.questionForm.get('timeToAnswer').setValue(question.secondsToAnswer);
      this.questionForm.get('position').setValue(question.position);
      this.questionForm.get('answerA').setValue(question.answers[0].text);
      this.questionForm.get('answerACorrect').setValue(question.answers[0].correct);
      this.questionForm.get('answerB').setValue(question.answers[1].text);
      this.questionForm.get('answerBCorrect').setValue(question.answers[1].correct);
      this.questionForm.get('answerC').setValue(question.answers[2].text);
      this.questionForm.get('answerCCorrect').setValue(question.answers[2].correct);
      this.questionForm.get('answerD').setValue(question.answers[3].text);
      this.questionForm.get('answerDCorrect').setValue(question.answers[3].correct);
    });
  }

  modifyQuestion(): void {
    let id = +this.route.snapshot.paramMap.get('questionid');
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
    this.questionService.patchQuestion(this.question, id).subscribe(res => {
      this.router.navigate(['../../'], { relativeTo: this.route });
    });
  }
}
