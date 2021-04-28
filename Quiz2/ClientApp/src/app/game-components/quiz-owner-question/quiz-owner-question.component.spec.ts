import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizOwnerQuestionComponent } from './quiz-owner-question.component';

describe('QuizOwnerQuestionComponent', () => {
  let component: QuizOwnerQuestionComponent;
  let fixture: ComponentFixture<QuizOwnerQuestionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizOwnerQuestionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizOwnerQuestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
