import {Answer} from "./answer";


export interface CurrentQuestionStat {
  correctedAnswers: Answer[];
  stats: number[];
  point: number;
}
