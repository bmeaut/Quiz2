import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizFailedJoinComponent } from './quiz-failed-join.component';

describe('QuizFailedJoinComponent', () => {
  let component: QuizFailedJoinComponent;
  let fixture: ComponentFixture<QuizFailedJoinComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizFailedJoinComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizFailedJoinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
