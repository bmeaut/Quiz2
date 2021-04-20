import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-quiz-question-edit',
  templateUrl: './quiz-question-edit.component.html',
  styleUrls: ['./quiz-question-edit.component.css']
})
export class QuizQuestionEditComponent implements OnInit {

  questionEditForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router
  ) { 
    this.questionEditForm = this.formBuilder.group({
      question: ['', [Validators.required]],
      point: ['', [Validators.required]],
      time: ['', [Validators.required]],
      number: ['', [Validators.required]]
    });
  }

  ngOnInit() {
  }

  onSubmit(): void {
    this.router.navigate(['/question_list']);
  }

}