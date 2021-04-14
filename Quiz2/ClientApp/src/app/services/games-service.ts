import * as signalR from "@microsoft/signalr";
import {Question} from "../question";
import {ComponentFactoryResolver, EventEmitter, Injectable} from "@angular/core";
import {QuizGameComponent} from "../quiz-game/quiz-game.component";
import {AuthorizeService} from "../../api-authorization/authorize.service";
import {async} from "@angular/core/testing";

@Injectable()
export class GamesService {

  private connection;

  public joinedToGame = new EventEmitter();
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

      this.connection.on("joined", () => {
        this.joinedToGame.emit();
        console.debug("siker");
      });
      this.connection.on("joinFailed", () => {
        console.debug("rossz");
      });

      this.connection.on("started", () => {
        console.debug("elindult");
      });

      this.connection.on("startFailed", () => {
        console.debug("nem indult el");
      });
      this.connection.on("newQuestion", (question :Question) => {
        //   this.gameComponent.loadQuizQuestionComponent()
        console.debug(question);
      });
      this.connection.start().catch(err => document.write(err));
    });
  }

  joinGroup(joinId: string){

    this.connection.send("JoinGame", joinId);
    console.debug("group1");
  }

  keres(){
    this.connection.send("GroupTest");
    console.debug("keres");
  }

  nextQuestion(){
    this.connection.send("NextQuestion", "ProbaID");
    console.debug("új kérdés");
  }

  startGame(){
    this.connection.send("StartGame", (<HTMLInputElement>document.getElementById("input_join_id")).value);
    console.debug("start");
  }
}
