import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizOwnerLobbyComponent } from './quiz-owner-lobby.component';

describe('QuizOwnerLobbyComponent', () => {
  let component: QuizOwnerLobbyComponent;
  let fixture: ComponentFixture<QuizOwnerLobbyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizOwnerLobbyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizOwnerLobbyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
