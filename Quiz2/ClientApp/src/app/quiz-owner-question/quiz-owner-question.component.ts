import { Component, OnInit } from '@angular/core';
import { Subscription, timer } from 'rxjs';
import { Question } from '../question';
import {GamesService} from "../services/games-service";

@Component({
  selector: 'app-quiz-owner-question',
  templateUrl: './quiz-owner-question.component.html',
  styleUrls: ['./quiz-owner-question.component.css']
})
export class QuizOwnerQuestionComponent implements OnInit {

  subscriptionToTimer: Subscription;
  displayTime: string;
  timeIsOver: boolean;

  question: Question = {
    id: 1,
    quizId: 1,
    quiz: { id: 1, name: "", questions: [], owner: { id: "" }, games: [] },
    text: "Ez egy kérdés?",
    secondsToAnswer: 5,
    position: 1,
    points: 5,
    answers:
    [{id: 1, correct: true, text: "Válasz1", questionId: 1},
     {id: 2, correct: false, text: "Válasz2", questionId: 1},
     {id: 3, correct: false, text: "Válasz3", questionId: 1},
     {id: 4, correct: false, text: "Válasz4", questionId: 1}]};

  constructor(public gameService: GamesService) { }

  ngOnInit() {
    this.startTimer();
  }

  startTimer(): void {
    let timeToAnswer = this.question.secondsToAnswer;
    timeToAnswer += 1;
    let timerObservable = timer(0,1000);
    this.subscriptionToTimer = timerObservable.subscribe(() => {
      timeToAnswer--;
      console.log(timeToAnswer);
      if(timeToAnswer >= 0) {
        let minutes = Math.floor(timeToAnswer % 3600 / 60);
        let seconds = Math.floor(timeToAnswer % 3600 % 60);
        
        this.displayTime = minutes < 10 ? "0" + minutes + ":" : minutes + ":";
        this.displayTime += seconds < 10 ? "0" + seconds : seconds;
      } else {
        this.subscriptionToTimer.unsubscribe();
        this.timeIsOver = true;
      }
    });
  }

}
