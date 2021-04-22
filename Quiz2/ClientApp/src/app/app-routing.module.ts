import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthorizeGuard } from "../api-authorization/authorize.guard";
import { FetchDataComponent } from "./fetch-data/fetch-data.component";
import { HomeComponent } from "./home/home.component";
import { PageNotFoundComponent } from "./page-not-found/page-not-found.component";
import { QuizCreateComponent } from "./quiz-create/quiz-create.component";
import { QuizEditComponent } from "./quiz-edit/quiz-edit.component";
import { QuizGameComponent } from "./quiz-game/quiz-game.component";
import { QuizListComponent } from "./quiz-list/quiz-list.component";
import { QuizOwnerLobbyComponent } from "./quiz-owner-lobby/quiz-owner-lobby.component";
import { QuizOwnerQuestionComponent } from "./quiz-owner-question/quiz-owner-question.component";
import { QuizOwnerStatisticsComponent } from "./quiz-owner-statistics/quiz-owner-statistics.component";
import { QuizQuestionCreateComponent } from "./quiz-question-create/quiz-question-create.component";
import { QuizQuestionEditComponent } from "./quiz-question-edit/quiz-question-edit.component";
import { QuizQuestionListComponent } from "./quiz-question-list/quiz-question-list.component";
import { SRTestComponent } from "./SR-test/SR-test.component";

const appRoutes: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
    { path: 'sr-test', component: SRTestComponent },
    { path: 'question', component: QuizOwnerQuestionComponent},
    { path: 'lobby', component: QuizOwnerLobbyComponent},
    { path: 'game', component: QuizGameComponent},
    { path: 'stats', component: QuizOwnerStatisticsComponent},
    { path: 'questions', component: QuizQuestionListComponent, canActivate: [AuthorizeGuard] },
    { path: 'questions/new', component: QuizQuestionCreateComponent, canActivate: [AuthorizeGuard]},
    { path: 'questions/:id/edit', component: QuizQuestionEditComponent, canActivate: [AuthorizeGuard]},
    { path: 'quizzes', component: QuizListComponent, canActivate: [AuthorizeGuard]},
    { path: 'quizzes/new', component: QuizCreateComponent, canActivate: [AuthorizeGuard]},
    { path: 'quizzes/:id/edit', component: QuizEditComponent, canActivate: [AuthorizeGuard]},
    { path: 'not-found', component: PageNotFoundComponent },
    { path: '**', redirectTo: '/not-found' }
  ];
  
  @NgModule({
    imports: [
      RouterModule.forRoot(appRoutes)
    ],
    exports: [RouterModule]
  })
  export class AppRoutingModule {
  
  }