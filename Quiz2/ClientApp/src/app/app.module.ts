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

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    QuizQuestionComponent,
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
    PlaceholderDirective
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'question', component: QuizQuestionComponent},
      { path: 'lobby', component: QuizLobbyComponent},
      { path: 'stats', component: QuizStatisticsComponent},
      { path: 'question_list', component: QuizQuestionListComponent},
      { path: 'edit', component: QuizQuestionEditComponent},
      { path: 'game', component: QuizGameComponent},
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    QuizOwnerLobbyComponent,
    QuizOwnerQuestionComponent,
    QuizOwnerStatisticsComponent
  ]
})
export class AppModule { }
