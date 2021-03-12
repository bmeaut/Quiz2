import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizStatisticsComponent } from './quiz-statistics.component';

describe('QuizStatisticsComponent', () => {
  let component: QuizStatisticsComponent;
  let fixture: ComponentFixture<QuizStatisticsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizStatisticsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizStatisticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
