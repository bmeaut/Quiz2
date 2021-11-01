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
import {QuizFailedJoinComponent} from "../quiz-failed-join/quiz-failed-join.component";
import {QuizWaitingComponent} from "../quiz-waiting/quiz-waiting.component";
import {OwnerJoinedToStartedDto} from "../../ownerJoinedToStartedDto";

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
        this.loadQuizLobbyComponent();
    });

    this.gameService.ownerJoinedToGame.subscribe( () => {
      this.loadQuizOwnerLobbyComponent();
    });

    this.gameService.gameStartedOwner.subscribe( (question: Question) => {
      this.loadQuizOwnerQuestionComponent(question);
    });

    this.gameService.gameStarted.subscribe( (question: Question) => {
      this.loadQuizQuestionComponent(question);
      this.question= question;
    });

    this.gameService.newQuestion.subscribe( (question: Question) => {
      this.loadQuizQuestionComponent(question);
      this.question= question;
    });

    this.gameService.endQuestion.subscribe( (stat :CurrentQuestionStat) => {
      this.loadPlayerStatisticsComponent(stat);
    });

    this.gameService.gameNotExist.subscribe( () => {
      this.loadQuizFailedJoinComponent("Nincs ilyen játék.");
    });

    this.gameService.gameFinished.subscribe( () => {
      this.loadQuizFailedJoinComponent("A játék már véget ért.");
    });

    this.gameService.joinedToStarted.subscribe( () => {
      this.loadQuizWaitingComponent();
    });

    this.gameService.ownerJoinedToStarted.subscribe( (ownerJoinedToStartedDto: OwnerJoinedToStartedDto) => {
      this.loadQuizOwnerQuestionComponentWhenStarted(ownerJoinedToStartedDto);
    });
  }

  loadQuizQuestionComponent(question: Question) {
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
  loadQuizOwnerQuestionComponentWhenStarted(ownerJoinedToStartedDto: OwnerJoinedToStartedDto) {
    const quizOwnerQuestionFactory = this.cfr.resolveComponentFactory(QuizOwnerQuestionComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    const quizOwnerQuestionComponent = <QuizOwnerQuestionComponent>hostViewContainerRef.createComponent(quizOwnerQuestionFactory).instance;
    quizOwnerQuestionComponent.question=ownerJoinedToStartedDto.question;
    if(ownerJoinedToStartedDto.remainingTime <= 0){
      quizOwnerQuestionComponent.question.secondsToAnswer = 0;
      quizOwnerQuestionComponent.disableNextQuestion = false;
    } else{
      quizOwnerQuestionComponent.question.secondsToAnswer = ownerJoinedToStartedDto.remainingTime;
    }
    quizOwnerQuestionComponent.question.secondsToAnswer
    quizOwnerQuestionComponent.stats=ownerJoinedToStartedDto.currentQuestionStat.stats;
  }

  loadQuizOwnerLobbyComponent() {
    const quizOwnerLobbyComponentFactory = this.cfr.resolveComponentFactory(QuizOwnerLobbyComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    hostViewContainerRef.createComponent(quizOwnerLobbyComponentFactory);
  }

  loadPlayerStatisticsComponent(currentQuestionStat :CurrentQuestionStat) {
    const playerStatisticsComponentFactory = this.cfr.resolveComponentFactory(PlayerStatisticsComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    const playerStatisticsComponent = <PlayerStatisticsComponent>hostViewContainerRef.createComponent(playerStatisticsComponentFactory).instance;
    playerStatisticsComponent.currentQuestionStat = currentQuestionStat;
    playerStatisticsComponent.question = this.question;
    playerStatisticsComponent.question.answers = currentQuestionStat.correctedAnswers;
  }

  loadQuizFailedJoinComponent(errorText: string) {
    const quizFailedJoinComponentFactory = this.cfr.resolveComponentFactory(QuizFailedJoinComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    const quizFailedJoinComponent = <QuizFailedJoinComponent>hostViewContainerRef.createComponent(quizFailedJoinComponentFactory).instance;
    quizFailedJoinComponent.errorText = errorText;
  }

  loadQuizWaitingComponent() {
    const quizWaitingComponentFactory = this.cfr.resolveComponentFactory(QuizWaitingComponent);
    const hostViewContainerRef = this.gameHost.viewContainerRef;
    hostViewContainerRef.clear();
    hostViewContainerRef.createComponent(quizWaitingComponentFactory);
  }

}
