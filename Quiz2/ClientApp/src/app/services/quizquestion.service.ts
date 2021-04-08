import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Question } from "../question";

@Injectable({
    providedIn: 'root'
})
export class QuizQuestionService {

    constructor(private httpClient: HttpClient) { }

    getQuestions() {
        return this.httpClient.get<Question[]>("");
    }

    getQuestion(id: number) {
        return this.httpClient.get<Question>("");
    }

    postQuestion(question: Question) {
        return this.httpClient.post<Question>("", question);
    }

    deleteQuestion(id: number) {
        return this.httpClient.delete<Question>("");
    }
}