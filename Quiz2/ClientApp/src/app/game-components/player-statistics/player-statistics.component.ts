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

  question: Question;

  answersSubmittedByThePlayer: {
    id: number,
    questionID: number,
    correct: boolean
  }[];

  letters: string[];

  currentQuestionStat: CurrentQuestionStat;


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
            data: this.currentQuestionStat.stats,
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
