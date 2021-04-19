import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Question } from "../question";
import { Quiz } from "../quiz";

@Injectable({
    providedIn: 'root'
})
export class QuizService {

    modifiedQuiz: Quiz = {
      id: 1,
      name: "",
      questions: [],
      owner: { id: "" },
      games: []
    };

    questions: Question[] = [{
      id: 1,
      quiz: { id: 1, name: "", questions: [], owner: {id: ""}, games: []},
      text: "",
      answers: [],
      secondsToAnswer: 0,
      position: 0,
      points: 0
    }];

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') readonly baseUrl: string) { }

    setModifiedQuiz(quiz: Quiz): void {
      this.modifiedQuiz = quiz;
    } 

    getModifiedQuiz(): Quiz {
      return this.modifiedQuiz;
    }

    setQuestions(questions: Question[]): void {
      this.questions = questions;
    }

    getQuestions(): Question[] {
      return this.questions;
    }

    getQuizzes() {
       return this.httpClient.get<Quiz[]>(this.baseUrl + "api/Quiz");
    }

    getQuiz(id: number) {
       return this.httpClient.get<Quiz>(this.baseUrl + "api/Quiz/" + id);
    }

    editQuiz(quiz: Quiz) {
      return this.httpClient.put<Quiz>(this.baseUrl + "api/Quiz/" + quiz.id, quiz);
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
