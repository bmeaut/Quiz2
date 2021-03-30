import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-quiz-statistics',
  templateUrl: './quiz-statistics.component.html',
  styleUrls: ['./quiz-statistics.component.css']
})
export class QuizStatisticsComponent implements OnInit {

  chart: Chart;

  constructor() { }

  ngOnInit() {
    this.drawChart();
  }

  drawChart() {
    this.chart = new Chart('canvas', {
      type: 'bar',
      data: {
        labels: ['Válasz1', 'Válasz2', 'Válasz3', 'Válasz4'],
        datasets: [
          {
            data: [6,3,4,10],
            backgroundColor: [
              '#737373',
              '#F2F2F2',
              '#F2DBD5',
              '#D9B2A9'
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
    }
    });
  }

}
