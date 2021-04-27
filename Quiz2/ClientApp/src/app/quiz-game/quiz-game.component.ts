import {Component, ComponentFactoryResolver, Injectable, OnInit, ViewChild} from '@angular/core';
import { PlaceholderDirective } from 'src/directives/placeholder.directive';
import { QuizLobbyComponent } from '../quiz-lobby/quiz-lobby.component';
import { QuizOwnerLobbyComponent } from '../quiz-owner-lobby/quiz-owner-lobby.component';
import { QuizOwnerQuestionComponent } from '../quiz-owner-question/quiz-owner-question.component';
import { QuizOwnerStatisticsComponent } from '../quiz-owner-statistics/quiz-owner-statistics.component';
import { QuizQuestionComponent } from '../quiz-question/quiz-question.component';
import { QuizStatisticsComponent } from '../quiz-statistics/quiz-statistics.component';
import {GamesService} from "../services/games-service";
import {Question} from "../question";

@Component({
  selector: 'app-quiz-game',
  templateUrl: './quiz-game.component.html',
  styleUrls: ['./quiz-game.component.css'],
})
export class QuizGameComponent implements OnInit {

  @ViewChild(PlaceholderDirective, {static: true}) gameHost: PlaceholderDirective;

  constructor(private cfr: ComponentFactoryResolver, public gameService: GamesService) { }

  ngOnInit() {

    this.gameService.joinedToGame.subscribe( () => {
      console.debug("lobby betöltése")
        this.loadQuizLobbyComponent();
    });
    this.gameService.ownerJoinedToGame.subscribe( () => {
      console.debug("owner lobby betöltése")
      this.loadQuizOwnerLobbyComponent();
    });
    this.gameService.gameStartedOwner.subscribe( (question: Question) => {
      console.debug("owner question betöltése")
      this.loadQuizOwnerQuestionComponent(question);
    });
    this.gameService.gameStarted.subscribe( (question: Question) => {
      console.debug("question betöltése")
      this.loadQuizQuestionComponent(question);
    });
    console.debug("ngOnInit vége")
  }
  loadQuizQuestionComponent(question: Question) {
    console.debug("loadQuizQuestionComponent "+ this.gameService.getJoinId())
    const questionComponentFactory = this.cfr.resolveComponentFactory(QuizQuestionComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    const quizQuestionComponent = <QuizQuestionComponent>hostViewContainerRef.createComponent(questionComponentFactory).instance;
    quizQuestionComponent.question=question;
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

  loadQuizOwnerQuestionComponent(question: Question) {
    const quizOwnerQuestionFactory = this.cfr.resolveComponentFactory(QuizOwnerQuestionComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    const quizOwnerQuestionComponent = <QuizOwnerQuestionComponent>hostViewContainerRef.createComponent(quizOwnerQuestionFactory).instance;
    quizOwnerQuestionComponent.question=question;
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
