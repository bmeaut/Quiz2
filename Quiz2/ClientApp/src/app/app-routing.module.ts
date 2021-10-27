import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthorizeGuard } from "../api-authorization/authorize.guard";
import { FetchDataComponent } from "./fetch-data/fetch-data.component";
import { HomeComponent } from "./home/home.component";
import { JoinGameComponent } from "./join-game/join-game.component";
import { PageNotFoundComponent } from "./page-not-found/page-not-found.component";
import { QuizCreateComponent } from "./quiz-list-components/quiz-create/quiz-create.component";
import { QuizEditComponent } from "./quiz-list-components/quiz-edit/quiz-edit.component";
import { QuizGameComponent } from "./game-components/quiz-game/quiz-game.component";
import { QuizListComponent } from "./quiz-list-components/quiz-list/quiz-list.component";
import { QuizOwnerLobbyComponent } from "./game-components/quiz-owner-lobby/quiz-owner-lobby.component";
import { QuizOwnerQuestionComponent } from "./game-components/quiz-owner-question/quiz-owner-question.component";
import { QuizQuestionCreateComponent } from "./question-list-components/quiz-question-create/quiz-question-create.component";
import { QuizQuestionEditComponent } from "./question-list-components/quiz-question-edit/quiz-question-edit.component";
import { QuizQuestionListComponent } from "./question-list-components/quiz-question-list/quiz-question-list.component";
import { QuizQuestionComponent } from "./game-components/quiz-question/quiz-question.component";
import { SRTestComponent } from "./SR-test/SR-test.component";
import { StatListComponent } from "./stat-list-components/stat-list/stat-list.component";
import { StatOwnedDetailsComponent } from "./stat-game-details-components/stat-owned-details/stat-owned-details.component"
import { StatPlayedDetailsComponent } from "./stat-game-details-components/stat-played-details/stat-played-details.component";

const appRoutes: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
    { path: 'sr-test', component: SRTestComponent },
    { path: 'question', component: QuizQuestionComponent},
    { path: 'ownerquestion', component: QuizOwnerQuestionComponent},
    { path: 'lobby', component: QuizOwnerLobbyComponent},
    { path: 'game', component: QuizGameComponent},
    { path: 'join', component: JoinGameComponent, canActivate: [AuthorizeGuard]},
    { path: 'quizzes/:id/questions', component: QuizQuestionListComponent},
    { path: 'quizzes/:id/questions/new', component: QuizQuestionCreateComponent},
    { path: 'quizzes/:id/questions/:questionid/edit', component: QuizQuestionEditComponent},
    { path: 'quizzes', component: QuizListComponent},
    { path: 'quizzes/new', component: QuizCreateComponent},
    { path: 'quizzes/:id/edit', component: QuizEditComponent},
    { path: 'stats', component: StatListComponent},
    { path: 'stats/:id/detailsOwned', component: StatOwnedDetailsComponent},
    { path: 'stats/:id/detailsPlayed', component: StatPlayedDetailsComponent},
    { path: 'not-found', component: PageNotFoundComponent },
    { path: '**', redirectTo: '/not-found' },
  ];
  
  @NgModule({
    imports: [
      RouterModule.forRoot(appRoutes)
    ],
    exports: [RouterModule]
  })
  export class AppRoutingModule {
  
  }