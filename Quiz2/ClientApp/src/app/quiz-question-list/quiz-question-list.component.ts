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

  questions: Question[] = [];

  constructor(private questionService: QuizQuestionService, private quizService: QuizService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.getQuestionList();
  }

  newQuestion(): void{
    this.router.navigate(["new"], {relativeTo: this.route});
  }

  getQuestionList(): void {
    let id = +this.route.snapshot.paramMap.get('id');
    this.quizService.getQuestionsOfQuiz(id).subscribe(questions => {
      this.questions = questions;
    });
    this.isLoaded = true;
  }

  deleteQuestion(question: Question): void {
    this.questionService.deleteQuestion(question.id).subscribe(() => {
      this.getQuestionList();
    });
  }

}
