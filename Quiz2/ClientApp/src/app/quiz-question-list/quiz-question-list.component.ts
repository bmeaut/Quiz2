import { Component, OnInit } from '@angular/core';
import { Question } from '../question';
import { QuizService } from '../services/quiz.service';
import { QuizQuestionService } from '../services/quizquestion.service';

@Component({
  selector: 'app-quiz-question-list',
  templateUrl: './quiz-question-list.component.html',
  styleUrls: ['./quiz-question-list.component.css']
})
export class QuizQuestionListComponent implements OnInit {

  questions: Question[] = [ {id: 0,
                            text: "Ez egy kérdés?",
                            secondsToAnswer: 0,
                            position: 0,
                            points: 0,
                            numberOfCorrectAnswers: 0,
                            answers: [{id: 1, isCorrect: true, text: "Válasz1", questionId: 0},
                            {id: 2, isCorrect: false, text: "Válasz2", questionId: 0},
                            {id: 3, isCorrect: false, text: "Válasz3", questionId: 0},
                            {id: 4, isCorrect: false, text: "Válasz4", questionId: 0}]}];

  constructor(private questionService: QuizQuestionService, private quizService: QuizService) { }

  ngOnInit() {
    this.getQuestionList();
  }

  getQuestionList() {
    this.questions = this.quizService.getQuestions();
  }

  /*getQuestionList(): void {
    this.questionService.getQuestions().subscribe(questions => {
      this.questions = questions;
    });
  }*/

  deleteQuestion(question: Question): void {
    //this.questions.splice(this.questions.findIndex(_question => _question.id === question.id), 1);
    this.questionService.deleteQuestion(question.id).subscribe(() => {
      //this.getQuestionList();
    });
  }

}
