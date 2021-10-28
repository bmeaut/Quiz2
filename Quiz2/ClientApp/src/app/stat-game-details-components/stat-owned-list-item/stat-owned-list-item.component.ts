import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { User } from "../../user";
import { AuthorizeService } from "src/api-authorization/authorize.service";
import { StatService } from "src/app/services/stat-service";

@Component({
    selector: 'app-stat-owned-list-item',
    templateUrl: './stat-owned-list-item.component.html',
    styleUrls: ['./stat-owned-list-item.component.css'],
  })
  export class StatOwnedListItem implements OnInit {
  
    @Input() user: User;

    constructor( private route: ActivatedRoute, private router: Router, private statService: StatService, private authService: AuthorizeService) { }
  
    ngOnInit() {
    }
    
    showDetails(){
      this.router.navigate(["/stats", this.route.snapshot.paramMap.get('id') , "detailsOwned", this.user.id]);
    }
  
  }