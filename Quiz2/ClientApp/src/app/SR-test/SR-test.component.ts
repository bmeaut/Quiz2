import {AfterViewInit, Component, OnInit} from '@angular/core';

import {GamesService} from "../services/games-service";



@Component({
  selector: 'app-SR-test',
  templateUrl: './SR-test.component.html',
  styleUrls: ['./SR-test.component.css']
})
export class SRTestComponent implements OnInit, AfterViewInit {

  constructor(public gameService: GamesService) { }

  ngOnInit() {
  }
  ngAfterViewInit() {
  }


}
