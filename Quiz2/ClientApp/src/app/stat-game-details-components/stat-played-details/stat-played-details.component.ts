import { Component, OnInit } from "@angular/core";
import { AuthorizeService } from "src/api-authorization/authorize.service";
import { Question } from "src/app/question";
import { StatService } from "src/app/services/stat-service";
import { threadId } from "worker_threads";

@Component({
    selector: 'app-stat-played-details',
    templateUrl: './stat-played-details.component.html',
    styleUrls: ['./stat-played-details.component.css'],
  })
  export class StatPlayedDetailsComponent implements OnInit {
  
    questions: Question[];

    constructor(private statService: StatService, private authService: AuthorizeService) { }
  
    ngOnInit() {
      this.questions = [];
      this.getQuestionsOfGame();
    }
  
    getQuestionsOfGame(){
      this.statService.getQuestionsOfPlayedGame().subscribe(questions => {
        this.questions = questions;
      });
    }
  
  }
  