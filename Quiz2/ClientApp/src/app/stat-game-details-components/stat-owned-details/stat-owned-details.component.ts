import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { AuthorizeService } from "src/api-authorization/authorize.service";
import { StatService } from "src/app/services/stat-service";
import { User } from '../../User'

@Component({
    selector: 'app-stat-owned-details',
    templateUrl: './stat-owned-details.component.html',
    styleUrls: ['./stat-owned-details.component.css'],
  })
  export class StatOwnedDetailsComponent implements OnInit {
    
    users: User[];
    
    constructor(private statService: StatService, private authService: AuthorizeService, private route: ActivatedRoute) { }
  
    ngOnInit() {
      this.users = [];
      this.getPlayersOfGame();
      
    }
    getPlayersOfGame(){
      this.statService.getUsersOfPlayedGame(this.route.snapshot.paramMap.get('id')).subscribe(users => {
        this.users = users;
      });
    }
  
  }
  