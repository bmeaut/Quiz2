import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { QuizQuestionComponent } from './quiz-question/quiz-question.component';
import { QuizAnswerComponent } from './quiz-answer/quiz-answer.component';
import { QuizLobbyComponent } from './quiz-lobby/quiz-lobby.component';
import { QuizLobbyPlayerComponent } from './quiz-lobby-player/quiz-lobby-player.component';
import { QuizStatisticsComponent } from './quiz-statistics/quiz-statistics.component';
import { QuizQuestionEditComponent } from './quiz-question-edit/quiz-question-edit.component';
import { QuizQuestionListComponent } from './quiz-question-list/quiz-question-list.component';
import { QuizQuestionListItemComponent } from './quiz-question-list-item/quiz-question-list-item.component';
import { PlaceholderDirective } from 'src/directives/placeholder.directive';
import { QuizGameComponent } from './quiz-game/quiz-game.component';
import { QuizOwnerLobbyComponent } from './quiz-owner-lobby/quiz-owner-lobby.component';
import { QuizOwnerQuestionComponent } from './quiz-owner-question/quiz-owner-question.component';
import { QuizOwnerStatisticsComponent } from './quiz-owner-statistics/quiz-owner-statistics.component';
import {SRTestComponent} from "./SR-test/SR-test.component";
import {GamesService} from "./services/games-service";
import { QuizStatisticsAnswerComponent } from './quiz-statistics-answer/quiz-statistics-answer.component';
import { QuizListComponent } from './quiz-list/quiz-list.component';
import { QuizListItemComponent } from './quiz-list-item/quiz-list-item.component';
import { QuizEditComponent } from './quiz-edit/quiz-edit.component';
import { QuizCreateComponent } from './quiz-create/quiz-create.component';
import { QuizQuestionCreateComponent } from './quiz-question-create/quiz-question-create.component';
import { AppRoutingModule } from './app-routing.module';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    QuizQuestionComponent,
    QuizQuestionCreateComponent,
    QuizAnswerComponent,
    QuizLobbyComponent,
    QuizLobbyPlayerComponent,
    QuizStatisticsComponent,
    QuizQuestionEditComponent,
    QuizQuestionListComponent,
    QuizQuestionListItemComponent,
    FetchDataComponent,
    QuizGameComponent,
    QuizOwnerLobbyComponent,
    QuizOwnerQuestionComponent,
    QuizOwnerStatisticsComponent,
    QuizListComponent,
    QuizEditComponent,
    QuizCreateComponent,
    QuizListItemComponent,
    PlaceholderDirective,
    SRTestComponent,
    QuizStatisticsAnswerComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    ApiAuthorizationModule,
    AppRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    GamesService
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    QuizOwnerLobbyComponent,
    QuizOwnerQuestionComponent,
    QuizQuestionComponent,
    QuizOwnerStatisticsComponent,
    QuizLobbyComponent
  ]
})
export class AppModule { }
