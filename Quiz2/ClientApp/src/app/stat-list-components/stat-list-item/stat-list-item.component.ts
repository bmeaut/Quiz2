import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Game } from '../../game';
import { StatService } from '../../services/stat-service';

@Component({
  selector: 'app-stat-list-item',
  templateUrl: './stat-list-item.component.html',
  styleUrls: ['./stat-list-item.component.css'],
})
export class StatListItemComponent implements OnInit {

  @Input() game: Game;

  constructor(private router: Router, private statService: StatService) { }

  ngOnInit() {
    this.showGames(this.game)
  }

  showGames(game: Game): void {
    this.router.navigate(["/stats", this.game, "games"]);
  }

}
