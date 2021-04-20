import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Question } from "../question";
import { User } from "../user";

@Injectable({
    providedIn: 'root'
})
export class QuizUserService {

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') readonly baseUrl: string) { }

    getUser() {
        return this.httpClient.get<User>(this.baseUrl + "api/User");
    }
}
