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
import { ReactiveFormsModule } from '@angular/forms';
import { LoaderComponent } from './components/utilities/loader/loader.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { EnterpriseUsersComponent } from './components/enterprise/enterprise-users/enterprise-users.component';
import { EnterpriseUsersCreateUserComponent } from './components/enterprise/enterprise-users/enterprise-users-create-user/enterprise-users-create-user.component';
import { FormSegmentComponent } from './components/utilities/form-segment/form-segment.component';
import { FormInputComponent } from './components/utilities/form-input/form-input.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { NgbToast, NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastContainerComponent } from './components/utilities/toast-container/toast-container.component';
import { AuthConfigModule } from './auth/auth-config.module';

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
    ToastContainerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    NgbToastModule,
    AuthConfigModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}