import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';
import { Question } from 'src/app/question';
import {CurrentQuestionStat} from "../../currentQuestionStat";

@Component({
  selector: 'app-player-statistics',
  templateUrl: './player-statistics.component.html',
  styleUrls: ['./player-statistics.component.css']
})
export class PlayerStatisticsComponent implements OnInit {

  chart: Chart;

  question: Question = {
    text: "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Voluptas rerum vitae sed aliquid reprehenderit? Quisquam molestiae quaerat tempora id consequuntur!",
    answers: [{
      id: 0,
      questionID: 0,
      correct: true,
      text: "Lorem ipsum dolor sit amet."
    },
    {
      id: 0,
      questionID: 0,
      correct: false,
      text: "Lorem ipsum dolor sit amet."
    },
    {
      id: 0,
      questionID: 0,
      correct: false,
      text: "Lorem ipsum dolor sit amet."
    },
    {
      id: 0,
      questionID: 0,
      correct: false,
      text: "Lorem ipsum dolor sit amet."
    }],
    secondsToAnswer: 0,
    position: 0,
    points: 0,
  };

  answersSubmittedByThePlayer: {
    id: number,
    questionID: number,
    correct: boolean
  }[] = [{
    id: 0,
    questionID: 0,
    correct: false
  },
  {
    id: 0,
    questionID: 0,
    correct: false
  },
  {
    id: 0,
    questionID: 0,
    correct: true
  },
  {
    id: 0,
    questionID: 0,
    correct: false
  }];

  letters: string[];

  stat: CurrentQuestionStat;


  constructor() { }

  ngOnInit() {
    this.letters = ['A', 'B', 'C', 'D'];
    this.drawChart();
  }

  drawChart(): void {
    this.chart = new Chart('canvas', {
      type: 'doughnut',
      data: {
        labels: this.letters,
        datasets: [
          {
            data: this.stat.stats,
            backgroundColor: [
              '#D9D9D9',
              '#353D40',
              '#A1A5A6',
              '#003F63'
            ],
            fill: false,
          }
        ]
      },
      options: {
        responsive: true,
        legend: {
            display: true,
        },
        maintainAspectRatio: false
    }
    });
  }

}
