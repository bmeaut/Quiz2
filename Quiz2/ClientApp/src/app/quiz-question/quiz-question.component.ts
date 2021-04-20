import { Component, OnInit } from '@angular/core';
import { Question } from '../question';
import {GamesService} from "../services/games-service";

@Component({
  selector: 'app-quiz-question',
  templateUrl: './quiz-question.component.html',
  styleUrls: ['./quiz-question.component.css']
})
export class QuizQuestionComponent implements OnInit {

  question: Question = {
    id: 1,
    text: "Ez egy kérdés?",
    quiz: { id: 0, name: "", questions: [], owner: { id: "" }, games: [] },
    secondsToAnswer: 120,
    position: 1,
    points: 5,
    answers:
    [{id: 1, isCorrect: true, text: "Válasz1", questionId: 1},
     {id: 2, isCorrect: false, text: "Válasz2", questionId: 1},
     {id: 3, isCorrect: false, text: "Válasz3", questionId: 1},
     {id: 4, isCorrect: false, text: "Válasz4", questionId: 1}]};

  constructor( public gameService: GamesService) { }

  ngOnInit() {
    this.gameService.newQuestion.subscribe( (question: Question) => {
      console.debug("new question betöltése")
      this.question=question;
    });
  }

}
