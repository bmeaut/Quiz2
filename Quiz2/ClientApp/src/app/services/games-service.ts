import * as signalR from "@microsoft/signalr";
import {Question} from "../question";
import {ComponentFactoryResolver, EventEmitter, Injectable} from "@angular/core";
import {QuizGameComponent} from "../game-components/quiz-game/quiz-game.component";
import {AuthorizeService} from "../../api-authorization/authorize.service";
import {async} from "@angular/core/testing";
import {User} from "../user";
import {CurrentQuestionStat} from "../currentQuestionStat";
import {Answers} from "../answers";
import {OwnerJoinedToStartedDto} from "../ownerJoinedToStartedDto";
import {ActivatedRoute, Router} from "@angular/router";

@Injectable({
  providedIn: 'root',
})
export class GamesService {

  private connection;
  private joinId: string
  private started: boolean

  public joinedToGame = new EventEmitter();
  public ownerJoinedToGame = new EventEmitter();
  public gameStartedOwner = new EventEmitter<Question>();
  public gameStarted = new EventEmitter<Question>();
  public newQuestion = new EventEmitter<Question>();
  public newQuestionOwner = new EventEmitter<Question>();
  public endQuestion = new EventEmitter<CurrentQuestionStat>();
  public endQuestionOwner = new EventEmitter();
  public endGame = new EventEmitter();
  public newPlayer = new EventEmitter<User[]>();
  public currentQuestionStat = new EventEmitter<CurrentQuestionStat>();
  public gameNotExist = new EventEmitter();
  public gameFinished = new EventEmitter();
  public joinFailed = new EventEmitter();
  public startFailed = new EventEmitter();
  public ownerJoinedToStarted = new EventEmitter();
  public joinedToStarted = new EventEmitter<OwnerJoinedToStartedDto>();
  constructor(private authorizeService: AuthorizeService, private router: Router) {
    this.started = false;

    this.authorizeService.getAccessToken().subscribe(token => {
      this.connection = new signalR.HubConnectionBuilder().withUrl("/gamehub",
        {
             accessTokenFactory: () => token
          }
        )
        .build();

      this.connection.on("joined", (joinId: string) => {
        this.joinId = joinId;
        this.joinedToGame.emit();
      });

      this.connection.on("ownerJoined", (joinId: string) => {
        this.joinId = joinId;
        this.ownerJoinedToGame.emit();
      });

      this.connection.on("joinFailed", () => {
        this.joinFailed.emit();
      });

      this.connection.on("startedOwner", (question: Question) => {
        this.gameStartedOwner.emit(question);
      });

      this.connection.on("started", (question: Question) => {
        this.gameStarted.emit(question);
      });

      this.connection.on("startFailed", () => {
        this.startFailed.emit();
      });

      this.connection.on("newQuestion", (question :Question) => {
        this.newQuestion.emit(question);
      });

      this.connection.on("newQuestionOwner", (question :Question) => {
        this.newQuestionOwner.emit(question);
      });

      this.connection.on("endQuestion", (stat :CurrentQuestionStat) => {
        if(!this.started) {
          this.endQuestion.emit(stat);
        }
      });

      this.connection.on("endGame", (gameId: number) => {
        this.router.navigate(["/stats",gameId , "detailsPlayed"]);
      });

      this.connection.on("endGameOwner", (gameId: number) => {
        this.router.navigate(["/stats", gameId , "detailsOwned"]);
      });

      this.connection.on("newPlayer", (players :User[]) => {
        this.newPlayer.emit(players);
      });

      this.connection.on("currentQuestionStat", (stat :CurrentQuestionStat) => {
        this.currentQuestionStat.emit(stat);
      });

      this.connection.on("endQuestionOwner", () => {
        this.endQuestionOwner.emit();
      });

      this.connection.on("gameNotExist", () => {
        this.gameNotExist.emit();
      });

      this.connection.on("gameFinished", () => {
        this.gameFinished.emit();
      });

      this.connection.on("ownerJoinedToStarted", (ownerJoinedToStartedDto: OwnerJoinedToStartedDto) => {
        this.joinId = ownerJoinedToStartedDto.joinId;
        this.ownerJoinedToStarted.emit(ownerJoinedToStartedDto);
      });

      this.connection.on("joinedToStarted", (joinId: string) => {
        this.joinId =joinId;
        this.started = true;
        this.joinedToStarted.emit();
      });

      this.connection.start().catch(err => document.write(err));
    });
  }

  joinGame(joinId: string){
    this.connection.send("JoinGame", joinId);
  }

  nextQuestion(){
    this.connection.send("NextQuestion", this.joinId);
  }

  startGame(){
    this.connection.send("StartGame",  this.joinId);
  }

  sendAnswers(answers: Answers){
    this.connection.send("SendAnswers", this.joinId, answers);
  }

  createGame(quizId: number){
    this.connection.send("CreateGame",  quizId);
  }

  getJoinId(): string{
    return this.joinId;
  }

}
