import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Question } from '../../question';
import { QuizQuestionService } from '../../services/quizquestion.service';

@Component({
  selector: 'app-quiz-question-edit',
  templateUrl: './quiz-question-edit.component.html',
  styleUrls: ['./quiz-question-edit.component.css']
})
export class QuizQuestionEditComponent implements OnInit {

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
    this.getQuestion();
    console.log(this.question.answers[0].correct);
  }

  getQuestion(): void {
    let id = +this.route.snapshot.paramMap.get('questionid');
    this.questionService.getQuestion(id).subscribe((question: Question) => {
      this.question = question;
    });
  }

  modifyQuestion(): void {
    let question: Question = {
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
    question.text = this.question.text;
    question.answers = this.question.answers;
    question.points = this.question.points;
    question.position = this.question.position;
    question.secondsToAnswer = this.question.secondsToAnswer;
    console.log(question);
    let id = +this.route.snapshot.paramMap.get('questionid');
    this.questionService.patchQuestion(question, id).subscribe(res => {
      this.router.navigate(['../../'], { relativeTo: this.route });
    });
  }
}
