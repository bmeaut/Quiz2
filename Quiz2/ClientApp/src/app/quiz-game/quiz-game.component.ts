import { createTokenForExternalReference } from '@angular/compiler/src/identifiers';
import { Component, ComponentFactoryResolver, OnInit, ViewChild } from '@angular/core';
import { PlaceholderDirective } from 'src/directives/placeholder.directive';
import { QuizLobbyComponent } from '../quiz-lobby/quiz-lobby.component';
import { QuizQuestionComponent } from '../quiz-question/quiz-question.component';
import { QuizStatisticsComponent } from '../quiz-statistics/quiz-statistics.component';

@Component({
  selector: 'app-quiz-game',
  templateUrl: './quiz-game.component.html',
  styleUrls: ['./quiz-game.component.css']
})
export class QuizGameComponent implements OnInit {

  @ViewChild(PlaceholderDirective, {static: false}) gameHost: PlaceholderDirective;

  constructor(private cfr: ComponentFactoryResolver) { }

  ngOnInit() {
  }

  loadQuestionComponent() {
    const questionComponentFactory = this.cfr.resolveComponentFactory(QuizQuestionComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    hostViewContainerRef.createComponent(questionComponentFactory);
  }

  loadQuizStatistics() {
    const quizStatisticsFactory = this.cfr.resolveComponentFactory(QuizStatisticsComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    hostViewContainerRef.createComponent(quizStatisticsFactory);
  }

  loadQuizLobby() {
    const quizLobbyComponentFactory = this.cfr.resolveComponentFactory(QuizLobbyComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    hostViewContainerRef.createComponent(quizLobbyComponentFactory);
  }

}
