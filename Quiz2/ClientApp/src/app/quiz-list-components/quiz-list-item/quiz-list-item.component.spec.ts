import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizListItemComponent } from './quiz-list-item.component';

describe('QuizListItemComponent', () => {
  let component: QuizListItemComponent;
  let fixture: ComponentFixture<QuizListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizListItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
