import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TaskListComponent } from './pages/task/components/task-list/task-list.component';
import { NgbDropdownModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TaskComponent } from './pages/task/components/task/task.component';
import { HttpClientModule } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { taskListReducer } from './pages/task/store/reducers/task.reducer';
import { TaskEffects } from './pages/task/store/effects/task.effects';
import { AvatarComponent } from './core/components/avatar/avatar.component';
import { AvatarGroupComponent } from './core/components/avatar-group/avatar-group.component';
import { TaskStatusPipe } from './core/pipes/task-status.pipe';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DragDropModule } from '@angular/cdk/drag-drop';

@NgModule({
  declarations: [
    AppComponent,
    TaskListComponent,
    TaskComponent,
    AvatarComponent,
    AvatarGroupComponent,
    TaskStatusPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    StoreModule.forRoot({ tasks: taskListReducer }),
    EffectsModule.forRoot([TaskEffects]),
    FontAwesomeModule,
    NgbDropdownModule,
    BrowserAnimationsModule,
    DragDropModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
