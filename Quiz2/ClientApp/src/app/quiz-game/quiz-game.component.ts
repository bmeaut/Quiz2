import {Component, ComponentFactoryResolver, Injectable, OnInit, ViewChild} from '@angular/core';
import { PlaceholderDirective } from 'src/directives/placeholder.directive';
import { QuizLobbyComponent } from '../quiz-lobby/quiz-lobby.component';
import { QuizOwnerLobbyComponent } from '../quiz-owner-lobby/quiz-owner-lobby.component';
import { QuizOwnerQuestionComponent } from '../quiz-owner-question/quiz-owner-question.component';
import { QuizOwnerStatisticsComponent } from '../quiz-owner-statistics/quiz-owner-statistics.component';
import { QuizQuestionComponent } from '../quiz-question/quiz-question.component';
import { QuizStatisticsComponent } from '../quiz-statistics/quiz-statistics.component';
import {GamesService} from "../services/games-service";

@Component({
  selector: 'app-quiz-game',
  templateUrl: './quiz-game.component.html',
  styleUrls: ['./quiz-game.component.css'],
  providers:  [ GamesService ]
})
export class QuizGameComponent implements OnInit {

  @ViewChild(PlaceholderDirective, {static: true}) gameHost: PlaceholderDirective;

  constructor(private cfr: ComponentFactoryResolver, public gameService: GamesService) { }

  ngOnInit() {

    this.gameService.joinedToGame.subscribe( () => {
      console.debug("lobby betöltése")
        this.loadQuizLobbyComponent();
    });
  }
  loadQuizQuestionComponent() {
    const questionComponentFactory = this.cfr.resolveComponentFactory(QuizQuestionComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    hostViewContainerRef.createComponent(questionComponentFactory);
  }

  loadQuizStatisticsComponent() {
    const quizStatisticsFactory = this.cfr.resolveComponentFactory(QuizStatisticsComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    hostViewContainerRef.createComponent(quizStatisticsFactory);
  }

  loadQuizLobbyComponent() {
    const quizLobbyComponentFactory = this.cfr.resolveComponentFactory(QuizLobbyComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    hostViewContainerRef.createComponent(quizLobbyComponentFactory);
  }

  loadQuizOwnerQuestionComponent() {
    const quizOwnerQuestionFactory = this.cfr.resolveComponentFactory(QuizOwnerQuestionComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    hostViewContainerRef.createComponent(quizOwnerQuestionFactory);
  }

  loadQuizOwnerStatisticsComponent() {
    const quizOwnerStatisticsFactory = this.cfr.resolveComponentFactory(QuizOwnerStatisticsComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    hostViewContainerRef.createComponent(quizOwnerStatisticsFactory);
  }

  loadQuizOwnerLobbyComponent() {
    const quizOwnerLobbyComponentFactory = this.cfr.resolveComponentFactory(QuizOwnerLobbyComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    hostViewContainerRef.createComponent(quizOwnerLobbyComponentFactory);
  }

}
