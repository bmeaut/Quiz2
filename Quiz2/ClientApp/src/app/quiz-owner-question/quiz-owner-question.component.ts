import { Component, OnInit } from '@angular/core';
import { Question } from '../question';

@Component({
  selector: 'app-quiz-owner-question',
  templateUrl: './quiz-owner-question.component.html',
  styleUrls: ['./quiz-owner-question.component.css']
})
export class QuizOwnerQuestionComponent implements OnInit {

  question: Question = {
    id: 1, 
    quizId: 1, 
    text: "Ez egy kérdés?",
    timer: 120,
    position: 1,
    points: 5, 
    numberOfCorrectAnswers: 1,
    answers: 
    [{id: 1, isCorrect: true, text: "Válasz1", questionId: 1},
     {id: 2, isCorrect: false, text: "Válasz2", questionId: 1},
     {id: 3, isCorrect: false, text: "Válasz3", questionId: 1},
     {id: 4, isCorrect: false, text: "Válasz4", questionId: 1}]};

  constructor() { }

  ngOnInit() {
  }

}
