import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizQuestionEditComponent } from './quiz-question-edit.component';

describe('QuizQuestionEditComponent', () => {
  let component: QuizQuestionEditComponent;
  let fixture: ComponentFixture<QuizQuestionEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizQuestionEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizQuestionEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
