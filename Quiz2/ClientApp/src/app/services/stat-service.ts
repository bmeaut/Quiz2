import { HttpHeaders } from "@angular/common/http";
import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { GameStat } from "../game-stat";
import { Question } from "../question";

@Injectable({
    providedIn: 'root'
})
export class StatService {
    
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') readonly baseUrl: string) { }

  getOwnGames(){
      return this.httpClient.get<GameStat[]>(this.baseUrl + "api/Stat/GetOwnedGameHistory")
  }

  getPlayedGames(){
      return this.httpClient.get<GameStat[]>(this.baseUrl + "api/Stat/GetPlayedGameHistory")
  }

    getQuestionsOfPlayedGame(){
        return this.httpClient.get<Question[]>(this.baseUrl + "api/Stat/GetQuestionsOfPlayedGame")//gameId
    }
}