import * as signalR from "@microsoft/signalr";
import {Question} from "../question";
import {ComponentFactoryResolver, EventEmitter, Injectable} from "@angular/core";
import {QuizGameComponent} from "../game-components/quiz-game/quiz-game.component";
import {AuthorizeService} from "../../api-authorization/authorize.service";
import {async} from "@angular/core/testing";
import {User} from "../user";
import {CurrentQuestionStat} from "../currentQuestionStat";

@Injectable({
  providedIn: 'root',
})
export class GamesService {

  private connection;
  private joinId: string

  public joinedToGame = new EventEmitter();
  public ownerJoinedToGame = new EventEmitter();
  public gameStartedOwner = new EventEmitter<Question>();
  public gameStarted = new EventEmitter<Question>();
  public newQuestion = new EventEmitter<Question>();
  public endQuestion = new EventEmitter();
  public endGame = new EventEmitter();
  public newPlayer = new EventEmitter<User[]>();
  public currentQuestionStat = new EventEmitter<CurrentQuestionStat>();
  constructor(private authorizeService: AuthorizeService) {


    this.authorizeService.getAccessToken().subscribe(token => {
      this.connection = new signalR.HubConnectionBuilder().withUrl("/gamehub",
        {
             accessTokenFactory: () => token
          }
        )
        .build();

      this.connection.on("groupTestAnswer", (text: string) => {
        console.debug(text);
      });

      this.connection.on("joined", (joinId: string) => {
        this.joinId = joinId;
        this.joinedToGame.emit();
        console.debug("joined");
      });
      this.connection.on("ownerJoined", (joinId: string) => {
        this.joinId = joinId;
        this.ownerJoinedToGame.emit();
        console.debug("ownerJoined");
      });
      this.connection.on("joinFailed", () => {
        console.debug("rossz");
      });

      this.connection.on("startedOwner", (question: Question) => {
        this.gameStartedOwner.emit(question);
        console.debug("elindult");
      });

      this.connection.on("started", (question: Question) => {
        this.gameStarted.emit(question);
        console.debug("elindult");
      });
      this.connection.on("startFailed", () => {
        console.debug("nem indult el");
      });
      this.connection.on("newQuestion", (question :Question) => {
        this.newQuestion.emit(question)
        console.debug("newQuestion");
      });
      this.connection.on("endQuestion", () => {
        this.endQuestion.emit()
        console.debug("endQuestion");
      });
      this.connection.on("endGame", () => {
        this.endGame.emit()
        console.debug("endGame");
      });
      this.connection.on("newPlayer", (players :User[]) => {
        this.newPlayer.emit(players)
        console.debug("newPlayer");
      });
      this.connection.on("currentQuestionStat", (stat :CurrentQuestionStat) => {
        this.currentQuestionStat.emit(stat)
        console.debug("currentQuestionStat");
      });
      this.connection.start().catch(err => document.write(err));
    });
  }

  joinGame(joinId: string){

    this.connection.send("JoinGame", joinId);
    console.debug("joinGame " + joinId);
  }

  keres(){
    this.connection.send("GroupTest");
    console.debug("keres");
  }

  nextQuestion(){
    this.connection.send("NextQuestion", this.joinId);
    console.debug("új kérdés");
  }

  startGame(){
    this.connection.send("StartGame",  this.joinId);
    console.debug("startGame");
  }

  sendAnswer(answerId: number){
    this.connection.send("SendAnswer",  this.joinId, answerId);
    console.debug("sendAnswer "+this.joinId);
  }

  createGame(quizId: number){
    this.connection.send("CreateGame",  quizId);
    console.debug("createGame");
  }

  getJoinId(): string{
    return this.joinId;
  }

}
