import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Question } from '../question';
import { QuizQuestionService } from '../services/quizquestion.service';

@Component({
  selector: 'app-quiz-question-create',
  templateUrl: './quiz-question-create.component.html',
  styleUrls: ['./quiz-question-create.component.css']
})
export class QuizQuestionCreateComponent implements OnInit {

  question: Question = {
    id: 1,
    quizId: 1,
    quiz: { id: 1, name: "", questions: [], owner: { id: "" }, games: [] },
    text: "",
    answers: [{ id: 1, correct: false, text: "", questionId: 1 },
              { id: 1, correct: false, text: "", questionId: 1 },
              { id: 1, correct: false, text: "", questionId: 1 },
              { id: 1, correct: false, text: "", questionId: 1 }],
    secondsToAnswer: 0,
    position: 0,
    points: 0
  };

  constructor(private router: Router, private questionService: QuizQuestionService, private route: ActivatedRoute) { }

  ngOnInit() {
  }

  createQuestion() {
    let id = this.route.snapshot.paramMap.get('id');
    this.question.quizId = +id;
    this.questionService.putQuestion(this.question).subscribe(res => {
      this.router.navigate(['../'], { relativeTo: this.route });
    });
  }

}
