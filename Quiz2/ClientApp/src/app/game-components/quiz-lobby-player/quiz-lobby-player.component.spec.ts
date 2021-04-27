import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizLobbyPlayerComponent } from './quiz-lobby-player.component';

describe('QuizLobbyPlayerComponent', () => {
  let component: QuizLobbyPlayerComponent;
  let fixture: ComponentFixture<QuizLobbyPlayerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizLobbyPlayerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizLobbyPlayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
