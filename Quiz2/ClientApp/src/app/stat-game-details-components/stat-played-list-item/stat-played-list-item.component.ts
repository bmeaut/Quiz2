import { Component, Input, OnInit } from "@angular/core";
import { AuthorizeService } from "src/api-authorization/authorize.service";
import { Question } from "src/app/question";
import { StatService } from "src/app/services/stat-service";

@Component({
    selector: 'app-stat-played-list-item',
    templateUrl: './stat-played-list-item.component.html',
    styleUrls: ['./stat-played-list-item.component.css'],
  })
  export class StatPlayedListItem implements OnInit {
  
    @Input() question: Question;

    constructor(private statService: StatService, private authService: AuthorizeService) { }
  
    ngOnInit() {
    }
  
  
  }