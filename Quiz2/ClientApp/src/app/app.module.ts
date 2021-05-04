import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from '../../src/api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from '../../src/api-authorization/authorize.interceptor';
import { QuizQuestionComponent } from './game-components/quiz-question/quiz-question.component';
import { QuizAnswerComponent } from './game-components/quiz-answer/quiz-answer.component';
import { QuizLobbyComponent } from './game-components/quiz-lobby/quiz-lobby.component';
import { QuizLobbyPlayerComponent } from './game-components/quiz-lobby-player/quiz-lobby-player.component';
import { QuizQuestionEditComponent } from './question-list-components/quiz-question-edit/quiz-question-edit.component';
import { QuizQuestionListComponent } from './question-list-components/quiz-question-list/quiz-question-list.component';
import { QuizQuestionListItemComponent } from './question-list-components/quiz-question-list-item/quiz-question-list-item.component';
import { PlaceholderDirective } from '../../src/directives/placeholder.directive';
import { QuizGameComponent } from './game-components/quiz-game/quiz-game.component';
import { QuizOwnerLobbyComponent } from './game-components/quiz-owner-lobby/quiz-owner-lobby.component';
import { QuizOwnerQuestionComponent } from './game-components/quiz-owner-question/quiz-owner-question.component';
import {SRTestComponent} from "./SR-test/SR-test.component";
import {GamesService} from "./services/games-service";
import { QuizListComponent } from './quiz-list-components/quiz-list/quiz-list.component';
import { QuizListItemComponent } from './quiz-list-components/quiz-list-item/quiz-list-item.component';
import { QuizEditComponent } from './quiz-list-components/quiz-edit/quiz-edit.component';
import { QuizCreateComponent } from './quiz-list-components/quiz-create/quiz-create.component';
import { QuizQuestionCreateComponent } from './question-list-components/quiz-question-create/quiz-question-create.component';
import { AppRoutingModule } from './app-routing.module';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { JoinGameComponent } from './join-game/join-game.component';
import { PlayerStatisticsComponent } from './game-components/player-statistics/player-statistics.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    JoinGameComponent,
    QuizQuestionComponent,
    QuizQuestionCreateComponent,
    QuizAnswerComponent,
    QuizLobbyComponent,
    QuizLobbyPlayerComponent,
    QuizQuestionEditComponent,
    QuizQuestionListComponent,
    QuizQuestionListItemComponent,
    FetchDataComponent,
    PlayerStatisticsComponent,
    QuizGameComponent,
    QuizOwnerLobbyComponent,
    QuizOwnerQuestionComponent,
    QuizListComponent,
    QuizEditComponent,
    QuizCreateComponent,
    QuizListItemComponent,
    PlaceholderDirective,
    SRTestComponent,
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
    QuizLobbyComponent
  ]
})
export class AppModule { }
