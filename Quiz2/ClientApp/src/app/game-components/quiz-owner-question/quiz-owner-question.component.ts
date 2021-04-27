import { Component, OnInit } from '@angular/core';
import { Subscription, timer } from 'rxjs';
import { Question } from '../../question';
import { Chart } from 'chart.js';
import {GamesService} from "../../services/games-service";

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
    text: "Ez egy kérdés?",
    secondsToAnswer: 5,
    position: 1,
    points: 5,
    answers:
    [{id: 1, correct: true, text: "Válasz1"},
     {id: 2, correct: false, text: "Válasz2"},
     {id: 3, correct: false, text: "Válasz3"},
     {id: 4, correct: false, text: "Válasz4"}]};

  letters: string[];

  constructor(public gameService: GamesService) { }

  ngOnInit() {
    this.letters = ['A', 'B', 'C', 'D'];
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
        labels: ['A', 'B', 'C', 'D'],
        datasets: [
          {
            data: [6,3,4,10],
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

}
