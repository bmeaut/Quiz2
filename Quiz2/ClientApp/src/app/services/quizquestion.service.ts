import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Question } from "../question";

@Injectable({
    providedIn: 'root'
})
export class QuizQuestionService {

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') readonly baseUrl: string) { }

    getQuestions() {
        return this.httpClient.get<Question[]>(this.baseUrl + "");
    }

    getQuestion(id: number) {
        return this.httpClient.get<Question>(this.baseUrl + "");
    }

    putQuestion(question: Question) {
        return this.httpClient.put<Question>(this.baseUrl + "", question);
    }

    deleteQuestion(id: number) {
        return this.httpClient.delete<Question>(this.baseUrl + "");
    }
}
