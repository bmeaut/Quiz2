import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizQuestionCreateComponent } from './quiz-question-create.component';

describe('QuizQuestionCreateComponent', () => {
  let component: QuizQuestionCreateComponent;
  let fixture: ComponentFixture<QuizQuestionCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizQuestionCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizQuestionCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
