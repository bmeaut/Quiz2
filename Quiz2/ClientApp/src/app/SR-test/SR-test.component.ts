import {AfterViewInit, asNativeElements, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import { Question } from '../question';
import * as signalR from "@microsoft/signalr";



const connection = new signalR.HubConnectionBuilder().withUrl("/gamehub").build();
//const keres: HTMLButtonElement = document.querySelector("#keres");


connection.on("groupTestAnswer", (text: string) => {
 console.debug(text);
});

connection.on("joined", () => {
  console.debug("siker");
});
connection.on("joinFailed", () => {
  console.debug("rossz");
});

connection.on("started", () => {
  console.debug("elindult");
});

connection.on("startFailed", () => {
  console.debug("nem indult el");
});
connection.on("newQuestion", (question :Question) => {
  console.debug(question);
});
connection.start().catch(err => document.write(err));
@Component({
  selector: 'app-SR-test',
  templateUrl: './SR-test.component.html',
  styleUrls: ['./SR-test.component.css']
})
export class SRTestComponent implements OnInit, AfterViewInit {

  // question: string = "Ez egy kérdés?";
  // answers: string[] = ["Válasz1", "Válasz2", "Válasz3", "Válasz4"];
  // timer: number = 120;


  question: Question = {
    id: 1,
    quizId: 1,
    text: "Ez egy kérdés?",
    timer: 120,
    position: 1,
    points: 5,
    numberOfCorrectAnswers: 1,
    answers:
    [{id: 1, isCorrect: true, text: "Válasz1", questionId: 1},
     {id: 2, isCorrect: false, text: "Válasz2", questionId: 1},
     {id: 3, isCorrect: false, text: "Válasz3", questionId: 1},
     {id: 4, isCorrect: false, text: "Válasz4", questionId: 1}]};


  constructor() { }

  ngOnInit() {
  }
  ngAfterViewInit() {
  }

  joinGroup(){
    console.debug((<HTMLInputElement>document.getElementById("input_join_id")).value);
    connection.send("JoinGame", (<HTMLInputElement>document.getElementById("input_join_id")).value);
    console.debug("group1");
  }

  keres(){
    connection.send("GroupTest");
    console.debug("keres");
  }

  nextQuestion(){
    connection.send("NextQuestion", (<HTMLInputElement>document.getElementById("input_join_id")).value);
    console.debug("új kérdés");
  }

  startGame(){
    connection.send("StartGame", (<HTMLInputElement>document.getElementById("input_join_id")).value);
    console.debug("start");
  }
}
