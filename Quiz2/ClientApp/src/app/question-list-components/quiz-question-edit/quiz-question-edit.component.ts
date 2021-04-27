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
    text: "",
    answers: [{id: 0, questionID: 0, correct: false, text: ""},
    {id: 0, questionID: 0 ,correct: false, text: ""},
    {id: 0, questionID: 0,correct: false, text: ""},
    {id: 0, questionID: 0,correct: false, text: ""}
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
  }

  getQuestion(): void {
    let id = +this.route.snapshot.paramMap.get('questionid');
    this.questionService.getQuestion(id).subscribe(question => {
      this.question = question;
    });
  }

  modifyQuestion(): void {
    let id = +this.route.snapshot.paramMap.get('questionid');
    this.questionService.patchQuestion(this.question).subscribe(res => {
      this.router.navigate(['../../'], { relativeTo: this.route });
    });
  }

}
