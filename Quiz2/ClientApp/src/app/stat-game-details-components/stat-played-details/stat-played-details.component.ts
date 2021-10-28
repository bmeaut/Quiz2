import { Component, OnInit } from "@angular/core";
import { AuthorizeService } from "src/api-authorization/authorize.service";
import { Question } from "src/app/question";
import { StatService } from "src/app/services/stat-service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
    selector: 'app-stat-played-details',
    templateUrl: './stat-played-details.component.html',
    styleUrls: ['./stat-played-details.component.css'],
  })
  export class StatPlayedDetailsComponent implements OnInit {

    questions: Question[];

    constructor(private statService: StatService, private authService: AuthorizeService, private route: ActivatedRoute) { }

    ngOnInit() {
      this.questions = [];
      this.getQuestionsOfGame();
    }

    getQuestionsOfGame(){
      this.statService.getQuestionsOfPlayedGame(this.route.snapshot.paramMap.get('id')).subscribe(questions => {
        this.questions = questions;
      });
    }

  }
