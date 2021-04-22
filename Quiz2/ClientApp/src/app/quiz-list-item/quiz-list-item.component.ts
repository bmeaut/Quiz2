import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Quiz } from '../quiz';
import { QuizService } from '../services/quiz.service';

@Component({
  selector: 'app-quiz-list-item',
  templateUrl: './quiz-list-item.component.html',
  styleUrls: ['./quiz-list-item.component.css']
})
export class QuizListItemComponent implements OnInit {

  @Input() quiz: Quiz;
  @Output() deleteQuizListItem: EventEmitter<any> = new EventEmitter();

  constructor(private router: Router, private quizService: QuizService) { }

  ngOnInit() {
  }

  showQuestions(quiz: Quiz): void {
    this.quizService.getQuestionsOfQuiz(quiz.id).subscribe(questions => {
      this.quizService.setQuestions(questions);
      this.router.navigate(["/question_list"]);
    });
  }

  modifyQuiz(quiz: Quiz): void {
    this.quizService.getQuiz(quiz.id).subscribe(quiz => {
      this.quizService.setModifiedQuiz(quiz);
      this.router.navigate(["/edit-quiz"]);
    });
  }

  deleteItem(quiz: Quiz): void {
    this.deleteQuizListItem.emit(quiz);
  }
}