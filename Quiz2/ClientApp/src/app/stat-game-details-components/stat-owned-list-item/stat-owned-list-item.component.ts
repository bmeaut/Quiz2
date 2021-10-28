import { Component, Input, OnInit } from "@angular/core";
import { User } from "oidc-client";
import { AuthorizeService } from "src/api-authorization/authorize.service";
import { StatService } from "src/app/services/stat-service";

@Component({
    selector: 'app-stat-owned-list-item',
    templateUrl: './stat-owned-list-item.component.html',
    styleUrls: ['./stat-owned-list-item.component.css'],
  })
  export class StatOwnedListItem implements OnInit {
  
    @Input() user: User;

    constructor(private statService: StatService, private authService: AuthorizeService) { }
  
    ngOnInit() {
    }
    
    showDetails(game){

    }
  
  }