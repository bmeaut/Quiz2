import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Question } from "../question";

@Injectable({
    providedIn: 'root'
})
export class QuizQuestionService {

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') readonly baseUrl: string) { }

    getQuestion(id: number) {
        return this.httpClient.get<Question>(this.baseUrl + "api/Question/" + id);
    }

    putQuestion(question: Question) {
        return this.httpClient.put<Question>(this.baseUrl + "api/Question", question);
    }

    deleteQuestion(id: number) {
        return this.httpClient.delete<Question>(this.baseUrl + "api/Question/" + id);
    }
}
