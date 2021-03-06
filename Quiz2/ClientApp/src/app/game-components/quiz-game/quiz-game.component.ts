import {Component, ComponentFactoryResolver, Injectable, OnInit, ViewChild} from '@angular/core';
import { PlaceholderDirective } from '../../../directives/placeholder.directive';
import { QuizLobbyComponent } from '../quiz-lobby/quiz-lobby.component';
import { QuizOwnerLobbyComponent } from '../quiz-owner-lobby/quiz-owner-lobby.component';
import { QuizOwnerQuestionComponent } from '../quiz-owner-question/quiz-owner-question.component';
import { QuizQuestionComponent } from '../quiz-question/quiz-question.component';
import {GamesService} from "../../services/games-service";
import {Question} from "../../question";
import {PlayerStatisticsComponent} from "../player-statistics/player-statistics.component";
import {CurrentQuestionStat} from "../../currentQuestionStat";

@Component({
  selector: 'app-quiz-game',
  templateUrl: './quiz-game.component.html',
  styleUrls: ['./quiz-game.component.css'],
})
export class QuizGameComponent implements OnInit {

  @ViewChild(PlaceholderDirective, {static: true}) gameHost: PlaceholderDirective;

  private question: Question;
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
      this.question= question;
    });
    this.gameService.newQuestion.subscribe( (question: Question) => {
      console.debug("question betöltése")
      this.loadQuizQuestionComponent(question);
      this.question= question;
    });
    this.gameService.endQuestion.subscribe( (stat :CurrentQuestionStat) => {
      console.debug("player statistics betöltése")
      this.loadPlayerStatisticsComponent(stat);
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

  // loadQuizOwnerStatisticsComponent() {
  //   const quizOwnerStatisticsFactory = this.cfr.resolveComponentFactory(QuizOwnerStatisticsComponent);
  //   const hostViewContainerRef = this.gameHost.viewContainerRef;
  //   hostViewContainerRef.clear();
  //   hostViewContainerRef.createComponent(quizOwnerStatisticsFactory);
  // }

  loadQuizOwnerLobbyComponent() {
    const quizOwnerLobbyComponentFactory = this.cfr.resolveComponentFactory(QuizOwnerLobbyComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    hostViewContainerRef.createComponent(quizOwnerLobbyComponentFactory);
  }

  loadPlayerStatisticsComponent(stat :CurrentQuestionStat) {
    const playerStatisticsComponentFactory = this.cfr.resolveComponentFactory(PlayerStatisticsComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    const playerStatisticsComponent = <PlayerStatisticsComponent>hostViewContainerRef.createComponent(playerStatisticsComponentFactory).instance;
    playerStatisticsComponent.stats = stat.stats;
    playerStatisticsComponent.question = this.question;
  }

}
