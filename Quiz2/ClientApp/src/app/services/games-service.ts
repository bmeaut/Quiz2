import * as signalR from "@microsoft/signalr";
import {Question} from "../question";
import {Injectable} from "@angular/core";

@Injectable()
export class GamesService {

  private connection = new signalR.HubConnectionBuilder().withUrl("/gamehub").build();

  constructor() {
    this.connection.on("groupTestAnswer", (text: string) => {
      console.debug(text);
    });

    this.connection.on("joined", () => {
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
      console.debug(question);
    });
    this.connection.start().catch(err => document.write(err));
  }

  joinGroup(){
    console.debug((<HTMLInputElement>document.getElementById("input_join_id")).value);
    this.connection.send("JoinGame", (<HTMLInputElement>document.getElementById("input_join_id")).value);
    console.debug("group1");
  }

  keres(){
    this.connection.send("GroupTest");
    console.debug("keres");
  }

  nextQuestion(){
    this.connection.send("NextQuestion", (<HTMLInputElement>document.getElementById("input_join_id")).value);
    console.debug("új kérdés");
  }

  startGame(){
    this.connection.send("StartGame", (<HTMLInputElement>document.getElementById("input_join_id")).value);
    console.debug("start");
  }
}
