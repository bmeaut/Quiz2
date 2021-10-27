import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { GameStat } from 'src/app/game-stat';
import { StatService } from '../../services/stat-service';

@Component({
  selector: 'app-stat-list-item',
  templateUrl: './stat-list-item.component.html',
  styleUrls: ['./stat-list-item.component.css'],
})
export class StatListItemComponent implements OnInit {

  @Input() game: GameStat;

  constructor(private router: Router, private statService: StatService) { }

  ngOnInit() {
  }
  
  showDetails(game: GameStat): void {
    this.router.navigate(["/stats", this.game, "games"]);
  }

}
