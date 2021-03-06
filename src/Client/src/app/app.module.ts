import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SidebarComponent } from './layout/sidebar/sidebar.component';
import { NavbarComponent } from './layout/navbar/navbar.component';
import { ManagedUserCreateComponent } from './components/managed-user/managed-user-create/managed-user-create.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { LoginComponent } from './components/login/login.component';
import { EnterpriseSidebarEnterprisePickerComponent } from './components/enterprise/enterprise-sidebar-enterprise-picker/enterprise-sidebar-enterprise-picker.component';
import { EnterpriseSidebarMenuComponent } from './components/enterprise/enterprise-sidebar-menu/enterprise-sidebar-menu.component';
import { DepartmentSidebarMenuComponent } from './components/department/department-sidebar/department-sidebar-menu.component';
import { SidebarMenuActionComponent } from './components/sidebar/sidebar-menu-action/sidebar-menu-action.component';
import { EnterpriseCreateEnterpriseComponent } from './components/enterprise/enterprise-create-enterprise/enterprise-create-enterprise.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoaderComponent } from './components/utilities/loader/loader.component';
import {
  HttpClient,
  HttpClientModule,
  HTTP_INTERCEPTORS
} from '@angular/common/http';
import { EnterpriseUsersComponent } from './components/enterprise/enterprise-users/enterprise-users.component';
import { EnterpriseUsersCreateUserComponent } from './components/enterprise/enterprise-users/enterprise-users-create-user/enterprise-users-create-user.component';
import { FormSegmentComponent } from './components/utilities/form-segment/form-segment.component';
import { FormInputComponent } from './components/utilities/form-input/form-input.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import {
  NgbDropdownModule,
  NgbToastModule,
  NgbTooltip,
  NgbTooltipModule,
  NgbTypeahead,
  NgbTypeaheadModule
} from '@ng-bootstrap/ng-bootstrap';
import { ToastContainerComponent } from './components/utilities/toast-container/toast-container.component';
import { AuthConfigModule } from './auth/auth-config.module';
import { SidebarMenuComponent } from './layout/sidebar/sidebar-menu/sidebar-menu.component';
import { StartComponent } from './components/start/start.component';
import { StoreModule } from '@ngrx/store';
import { appUserReducer } from './store/reducers/app-user.reducer';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { UserComponent } from './components/user/user.component';
import { InviteComponent } from './components/user/invite/invite.component';
import { InviteAcceptModalComponent } from './components/user/invite-accept-modal/invite-accept-modal.component';
import { InviteUserModalComponent } from './components/enterprise/invite-user-modal/invite-user-modal.component';
import { DepartmentComponent } from './components/department/department.component';
import { DepartmentInfoComponent } from './components/department/department-info/department-info.component';
import { DepartmentUsersComponent } from './components/department/department-users/department-users.component';
import { UserSettingsComponent } from './components/user/user-settings/user-settings.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { ProjectComponent } from './components/project/project.component';
import { TaskComponent } from './components/task/task.component';
import { TaskAssignModalComponent } from './components/task/task-assign-modal/task-assign-modal.component';
import { ProjectUsersComponent } from './components/project/project-users/project-users.component';
import { TaskDetailsModalComponent } from './components/task/task-details-modal/task-details-modal.component';
import { MembersComponent } from './components/utilities/members/members.component';
import { AvatarPipe } from './pipes/avatar.pipe';
import { EnterpriseInvitesComponent } from './components/enterprise/enterprise-invites/enterprise-invites.component';
import { DepartmentCreateComponent } from './components/department/department-create/department-create.component';
import { ProjectCreateComponent } from './components/project/project-create/project-create.component';
import { DepartmentMembersComponent } from './components/utilities/department-members/department-members.component';
import { EffectsModule } from '@ngrx/effects';
import { TaskEffects } from './store/effects/task.effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { taskReducer } from './store/reducers/task.reducer';
import { TaskCreateModalComponent } from './components/task/task-create-modal/task-create-modal.component';
import { TaskTrackModalComponent } from './components/task/task-track-modal/task-track-modal.component';
import { TaskMembersComponent } from './components/task/task-members/task-members.component';
import { AddUserModalComponent } from './components/department/add-user-modal/add-user-modal.component';

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    NavbarComponent,
    ManagedUserCreateComponent,
    LoginComponent,
    DepartmentSidebarMenuComponent,
    EnterpriseSidebarEnterprisePickerComponent,
    EnterpriseSidebarMenuComponent,
    SidebarMenuActionComponent,
    EnterpriseCreateEnterpriseComponent,
    LoaderComponent,
    EnterpriseUsersComponent,
    EnterpriseUsersCreateUserComponent,
    FormSegmentComponent,
    FormInputComponent,
    ToastContainerComponent,
    SidebarMenuComponent,
    StartComponent,
    UserComponent,
    InviteComponent,
    InviteAcceptModalComponent,
    InviteUserModalComponent,
    DepartmentComponent,
    DepartmentInfoComponent,
    DepartmentUsersComponent,
    UserSettingsComponent,
    ProjectComponent,
    TaskComponent,
    TaskAssignModalComponent,
    ProjectUsersComponent,
    TaskDetailsModalComponent,
    MembersComponent,
    AvatarPipe,
    EnterpriseInvitesComponent,
    DepartmentCreateComponent,
    ProjectCreateComponent,
    DepartmentMembersComponent,
    TaskCreateModalComponent,
    TaskTrackModalComponent,
    TaskMembersComponent,
    AddUserModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    NgbToastModule,
    NgbDropdownModule,
    NgbTooltipModule,
    AuthConfigModule,
    StoreModule.forRoot({
      appUser: appUserReducer,
      tasks: taskReducer
    }),
    ImageCropperModule,
    NgbTypeaheadModule,
    EffectsModule.forRoot([TaskEffects]),
    StoreDevtoolsModule.instrument({
      maxAge: 25
    })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
