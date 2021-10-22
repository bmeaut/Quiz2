import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizWaitingComponent } from './quiz-waiting.component';

describe('QuizFailedJoinComponent', () => {
  let component: QuizWaitingComponent;
  let fixture: ComponentFixture<QuizWaitingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizWaitingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizWaitingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
