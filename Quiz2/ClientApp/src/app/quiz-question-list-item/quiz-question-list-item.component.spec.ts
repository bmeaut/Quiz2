import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizQuestionListItemComponent } from './quiz-question-list-item.component';

describe('QuizQuestionListItemComponent', () => {
  let component: QuizQuestionListItemComponent;
  let fixture: ComponentFixture<QuizQuestionListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizQuestionListItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizQuestionListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
