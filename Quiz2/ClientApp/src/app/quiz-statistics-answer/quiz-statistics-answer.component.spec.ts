import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizStatisticsAnswerComponent } from './quiz-statistics-answer.component';

describe('QuizStatisticsAnswerComponent', () => {
  let component: QuizStatisticsAnswerComponent;
  let fixture: ComponentFixture<QuizStatisticsAnswerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizStatisticsAnswerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizStatisticsAnswerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
