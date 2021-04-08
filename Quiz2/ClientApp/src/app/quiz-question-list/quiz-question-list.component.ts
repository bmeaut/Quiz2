import { Component, OnInit } from '@angular/core';
import { Question } from '../question';
import { QuizQuestionService } from '../services/quizquestion.service';

@Component({
  selector: 'app-quiz-question-list',
  templateUrl: './quiz-question-list.component.html',
  styleUrls: ['./quiz-question-list.component.css']
})
export class QuizQuestionListComponent implements OnInit {

  questions: Question[] = [ {id: 0,
                            quizId: 0,
                            text: "Ez egy kérdés?",
                            timer: 120,
                            position: 1,
                            points: 10,
                            numberOfCorrectAnswers: 1,
                            answers: [{id: 1, isCorrect: true, text: "Válasz1", questionId: 0},
                            {id: 2, isCorrect: false, text: "Válasz2", questionId: 0},
                            {id: 3, isCorrect: false, text: "Válasz3", questionId: 0},
                            {id: 4, isCorrect: false, text: "Válasz4", questionId: 0}]}];

  constructor(private quizService: QuizQuestionService) { }

  ngOnInit() {
    this.getQuestionList();
  }

  getQuestionList(): void {
    this.quizService.getQuestions().subscribe(questions => {
      this.questions = questions;
    });
  }

  deleteQuestion(question: Question): void {
    this.questions.splice(this.questions.findIndex(_question => _question.id === question.id), 1);
    this.quizService.deleteQuestion(question.id).subscribe(() => {
      this.getQuestionList();
    });
  }

}
