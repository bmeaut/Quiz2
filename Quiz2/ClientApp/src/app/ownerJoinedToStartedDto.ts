import {CurrentQuestionStat} from "./currentQuestionStat";
import {Question} from "./question";

export interface OwnerJoinedToStartedDto {
  question: Question;
  currentQuestionStat: CurrentQuestionStat
  remainingTime: number
  joinId: string
}
