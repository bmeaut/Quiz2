import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Question } from '../../question';
import { QuizQuestionService } from '../../services/quizquestion.service';

@Component({
  selector: 'app-quiz-question-create',
  templateUrl: './quiz-question-create.component.html',
  styleUrls: ['./quiz-question-create.component.css']
})
export class QuizQuestionCreateComponent implements OnInit {

  questionForm: FormGroup;
  question: Question = {
    id: 0,
    quizId: 0,
    text: "",
    answers: [{id: 0, questionID: 0, correct: false, text: ""},
    {id: 0, questionID: 0, correct: false, text: ""},
    {id: 0, questionID: 0, correct: false, text: ""},
    {id: 0, questionID: 0, correct: false, text: ""}],
    secondsToAnswer: 0,
    position: 0,
    points: 0
  };

  constructor(
    private router: Router, 
    private questionService: QuizQuestionService, 
    private route: ActivatedRoute
    ) { }

  ngOnInit() {
    this.questionForm = new FormGroup({
      'questiontext': new FormControl(null, [Validators.required, Validators.minLength(5), Validators.maxLength(300)]),
      'score': new FormControl(null, [Validators.required, Validators.min(0), Validators.pattern("^[0-9]*$"),]),
      'timeToAnswer': new FormControl(null, [Validators.required, Validators.min(5), Validators.pattern("^[0-9]*$")]),
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
  }

  createQuestion(): void {
    let id = this.route.snapshot.paramMap.get('id');
    this.question.quizId = +id;
    this.questionService.putQuestion(this.question).subscribe(res => {
      this.router.navigate(['../'], { relativeTo: this.route });
    });
  }
}
