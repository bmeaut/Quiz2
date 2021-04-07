import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';
import { Question } from '../question';

@Component({
  selector: 'app-quiz-statistics',
  templateUrl: './quiz-statistics.component.html',
  styleUrls: ['./quiz-statistics.component.css']
})
export class QuizStatisticsComponent implements OnInit {

  chart: Chart;
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
        maintainAspectRatio: false
    }
    });
  }

}
