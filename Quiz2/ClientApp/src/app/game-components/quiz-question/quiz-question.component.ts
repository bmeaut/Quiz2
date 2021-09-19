import { Component, OnInit } from '@angular/core';
import { Subscription, timer } from 'rxjs';
import { Answer } from '../../answer';
import { Question } from '../../question';
import {GamesService} from "../../services/games-service";
import {Answers} from "../../answers";

@Component({
  selector: 'app-quiz-question',
  templateUrl: './quiz-question.component.html',
  styleUrls: ['./quiz-question.component.css']
})
export class QuizQuestionComponent implements OnInit {

  subscriptionToTimer: Subscription;
  displayTime: string;
  timeIsOver: boolean = false;
  disableAnswers: boolean = false;
//answers: Answers;

  question: Question = {
    id: 1,
    quizId: 0,
    text: "Ez egy kérdés?",
    secondsToAnswer: 400,
    position: 1,
    points: 5,
    answers: [
      {id: 1, questionID: 0, correct: false, marked: false, text: "Válasz1"},
     {id: 2, questionID: 0, correct: false, marked: false, text: "Válasz2"},
     {id: 3, questionID: 0, correct: false, marked: false, text: "Válasz3"},
     {id: 4, questionID: 0, correct: false, marked: false, text: "Válasz4"}
     ]
  };

  constructor( public gameService: GamesService) {
  }

  ngOnInit() {
    this.gameService.newQuestion.subscribe( (question: Question) => {
      console.debug("new question betöltése")
      this.question=question;
      this.disableAnswers = false;
      this.timeIsOver = false;
      this.startTimer();
    });
    this.startTimer();
  }

  saveAnswer(): void
  {

    console.debug(this.question.answers.filter((answer) => {
      return answer.marked;
    }));
    const answers: Answers = {
      ids: this.question.answers.filter((answer) => {
        return answer.marked;
      }).map((answer) => answer.id)
    };
    console.debug(answers.ids);
    this.gameService.sendAnswer(answers);
    this.disableAnswers = true;

  }

  startTimer(): void {
    let timeToAnswer = this.question.secondsToAnswer;
    timeToAnswer += 1;
    let timerObservable = timer(0,1000);
    this.subscriptionToTimer = timerObservable.subscribe(() => {
      timeToAnswer--;
      if(timeToAnswer == 0) {
        this.timeIsOver = true;
        this.subscriptionToTimer.unsubscribe();
      }
      if(timeToAnswer >= 0) {
        let minutes = Math.floor(timeToAnswer % 3600 / 60);
        let seconds = Math.floor(timeToAnswer % 3600 % 60);

        this.displayTime = minutes < 10 ? "0" + minutes + ":" : minutes + ":";
        this.displayTime += seconds < 10 ? "0" + seconds : seconds;
      } else {
        this.subscriptionToTimer.unsubscribe();
      }
    });
  }

 /* answerIsCheckedHandler(checkedAnswer: Answer): void {
    if(this.answers.ids.includes(checkedAnswer.id)){
      var newAnswers: number[];
      this.answers.ids.forEach( answerId => {
            if(answerId != checkedAnswer.id){
              newAnswers.push(answerId);
            }
        });
        this.answers.ids = newAnswers;
    }else{
      this.answers.ids.push(checkedAnswer.id);
    }
  }*/

}

