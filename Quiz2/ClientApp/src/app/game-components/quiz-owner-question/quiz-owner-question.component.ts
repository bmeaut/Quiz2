import { Component, OnInit } from '@angular/core';
import { Subscription, timer } from 'rxjs';
import { Question } from '../../question';
import { Chart } from 'chart.js';
import {GamesService} from "../../services/games-service";
import {CurrentQuestionStat} from "../../currentQuestionStat";

@Component({
  selector: 'app-quiz-owner-question',
  templateUrl: './quiz-owner-question.component.html',
  styleUrls: ['./quiz-owner-question.component.css']
})
export class QuizOwnerQuestionComponent implements OnInit {

  chart: Chart;

  subscriptionToTimer: Subscription;
  displayTime: string;
  timeIsOver: boolean;

  question: Question = {
    id: 1,
    quizId: 0,
    text: "Ez egy kérdés?",
    secondsToAnswer: 5,
    position: 1,
    points: 5,
    answers:
    [{id: 1, questionID: 0, correct: true, text: "Válasz1"},
     {id: 2, questionID: 0, correct: false, text: "Válasz2"},
     {id: 3, questionID: 0,correct: false, text: "Válasz3"},
     {id: 4, questionID: 0,correct: false, text: "Válasz4"}]};

  letters: string[];
  stats: number[];
  disableNextQuestion = true;

  constructor(public gameService: GamesService) { }

  ngOnInit() {
    this.gameService.newQuestionOwner.subscribe( (question: Question) => {
      console.debug("new question betöltése")
      this.question=question;
      this.timeIsOver = false;
      this.stats = [0,0,0,0];
      if( this.chart) {
        this.chart.destroy();
      }
      this.drawChart();
      this.startTimer();
    });
    this.gameService.endQuestionOwner.subscribe(() =>{
        this.disableNextQuestion = false;
      })
    this.letters = ['A', 'B', 'C', 'D'];
    if(this.stats == undefined) {
      this.stats = [0, 0, 0, 0];
    }
    this.gameService.currentQuestionStat.subscribe((stat :CurrentQuestionStat) => {
      this.stats=stat.stats;
      this.chart.destroy();
      this.drawChart();
    })
    this.startTimer();
    this.drawChart();
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

  drawChart(): void {
    this.chart = new Chart('canvas', {
      type: 'bar',
      data: {
        labels: this.letters,
        datasets: [
          {
            data: this.stats,
            backgroundColor: [
              '#003F63',
              '#003F63',
              '#003F63',
              '#003F63'
            ],
            fill: false,
          }
        ]
      },
      options: {
        responsive: true,
        legend: {
            display: false,
        },
        maintainAspectRatio:false
    }
    });
  }

  nextQuestion(){
    this.gameService.nextQuestion();
    this.disableNextQuestion = true;
  }

}
