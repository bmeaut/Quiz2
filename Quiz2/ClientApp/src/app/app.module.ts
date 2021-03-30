import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

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
import {SRTestComponent} from "./SR-test/SR-test.component";

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
    SRTestComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'question', component: QuizQuestionComponent},
      { path: 'lobby', component: QuizLobbyComponent},
      { path: 'stats', component: QuizStatisticsComponent},
      { path: 'question_list', component: QuizQuestionListComponent},
      { path: 'edit', component: QuizQuestionEditComponent},
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
      { path: 'sr-test', component: SRTestComponent },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
