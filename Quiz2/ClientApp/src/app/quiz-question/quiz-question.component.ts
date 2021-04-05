import { Component, OnInit } from '@angular/core';
import { Question } from '../question';

@Component({
  selector: 'app-quiz-question',
  templateUrl: './quiz-question.component.html',
  styleUrls: ['./quiz-question.component.css']
})
export class QuizQuestionComponent implements OnInit {

  // question: string = "Ez egy kérdés?";
  // answers: string[] = ["Válasz1", "Válasz2", "Válasz3", "Válasz4"];
  // timer: number = 120;

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
