import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizOwnerStatisticsComponent } from './quiz-owner-statistics.component';

describe('QuizOwnerStatisticsComponent', () => {
  let component: QuizOwnerStatisticsComponent;
  let fixture: ComponentFixture<QuizOwnerStatisticsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizOwnerStatisticsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizOwnerStatisticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
