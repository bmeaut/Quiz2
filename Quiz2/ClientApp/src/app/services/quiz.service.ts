import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Question } from "../question";
import { Quiz } from "../quiz";

@Injectable({
    providedIn: 'root'
})
export class QuizService {

    questions: Question[] = [{
      id: 0,
      text: "",
      answers: [],
      secondsToAnswer: 0,
      position: 0,
      points: 0,
      numberOfCorrectAnswers: 0
    }];

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') readonly baseUrl: string) { }

    setQuestions(questions: Question[]) {
      this.questions = questions;
    }

    getQuizzes() {
       return this.httpClient.get<Quiz[]>(this.baseUrl + "api/Quiz");
    }

    getQuiz(id: number) {
       return this.httpClient.get<Quiz>(this.baseUrl + "");
    }

    putQuiz(quiz: Quiz) {
       return this.httpClient.put<Quiz>(this.baseUrl + "api/Quiz", quiz);
    }

    getQuestionsOfQuiz(id: number) {
      return this.httpClient.get<Question[]>(this.baseUrl + "api/Quiz/" + id + "/questions");
    }

    deleteQuiz(id: number) {
       return this.httpClient.delete<Quiz>(this.baseUrl + "api/Quiz/" + id);
    }
}
