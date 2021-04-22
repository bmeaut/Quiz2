import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Question } from '../question';
import { QuizQuestionService } from '../services/quizquestion.service';

@Component({
  selector: 'app-quiz-question-create',
  templateUrl: './quiz-question-create.component.html',
  styleUrls: ['./quiz-question-create.component.css']
})
export class QuizQuestionCreateComponent implements OnInit {

  question: Question = {
    id: 1,
    quiz: { id: 1, name: "", questions: [], owner: { id: "" }, games: [] },
    text: "",
    answers: [{ id: 1, isCorrect: false, text: "", questionId: 1 },
              { id: 1, isCorrect: false, text: "", questionId: 1 },
              { id: 1, isCorrect: false, text: "", questionId: 1 },
              { id: 1, isCorrect: false, text: "", questionId: 1 }],
    secondsToAnswer: 0,
    position: 0,
    points: 0
  };

  constructor(private router: Router, private questionService: QuizQuestionService) { }

  ngOnInit() {
  }

  createQuestion() {
    this.questionService.putQuestion(this.question).subscribe(() => {
      //this.router.navigate(["/quizzes"]);
    });
  }

}