import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Question } from '../question';
import { QuizService } from '../services/quiz.service';
import { QuizQuestionService } from '../services/quizquestion.service';

@Component({
  selector: 'app-quiz-question-list',
  templateUrl: './quiz-question-list.component.html',
  styleUrls: ['./quiz-question-list.component.css']
})
export class QuizQuestionListComponent implements OnInit {

  isLoaded: boolean = false;

  questions: Question[] = [{
                            id: 1,
                            quizId: 1,
                            quiz: { id: 1, name: "", questions: [], owner: { id: "" }, games: [] },
                            text: "Ez egy kérdés?",
                            secondsToAnswer: 0,
                            position: 0,
                            points: 0,
                            answers: [{id: 1, isCorrect: true, text: "Válasz1", questionId: 1},
                            {id: 2, isCorrect: false, text: "Válasz2", questionId: 1},
                            {id: 3, isCorrect: false, text: "Válasz3", questionId: 1},
                            {id: 4, isCorrect: false, text: "Válasz4", questionId: 1}]}];

  constructor(private questionService: QuizQuestionService, private quizService: QuizService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.getQuestionList();
  }

  newQuestion() {
    this.router.navigate(["new"], {relativeTo: this.route});
  }

  getQuestionList() {
    this.questions = this.quizService.getQuestions();
    this.isLoaded = true;
  }

  /*getQuestionList(): void {
    this.questionService.getQuestions().subscribe(questions => {
      this.questions = questions;
    });
  }*/

  deleteQuestion(question: Question): void {
    this.questionService.deleteQuestion(question.id).subscribe(() => {
      this.getQuestionList();
    });
  }

}
