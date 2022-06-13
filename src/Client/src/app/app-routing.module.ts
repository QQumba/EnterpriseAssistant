import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DepartmentInfoComponent } from './components/department/department-info/department-info.component';
import { DepartmentUsersComponent } from './components/department/department-users/department-users.component';
import { DepartmentComponent } from './components/department/department.component';
import { EnterpriseCreateEnterpriseComponent } from './components/enterprise/enterprise-create-enterprise/enterprise-create-enterprise.component';
import { EnterpriseUsersComponent } from './components/enterprise/enterprise-users/enterprise-users.component';
import { StartComponent } from './components/start/start.component';
import { InviteComponent } from './components/user/invite/invite.component';
import { UserSettingsComponent } from './components/user/user-settings/user-settings.component';
import { EnterpriseUserGuard } from './guards/enterprise-user.guard';
import { NewUserGuard } from './guards/new-user.guard';

const routes: Routes = [
  {
    path: 'user/settings',
    component: UserSettingsComponent,
    canActivate: [EnterpriseUserGuard]
  },
  {
    path: 'user/invite',
    component: InviteComponent,
    canActivate: [EnterpriseUserGuard]
  },
  {
    path: 'department/:id/users',
    component: DepartmentUsersComponent,
    canActivate: [EnterpriseUserGuard]
  },
  {
    path: 'department/:id',
    component: DepartmentInfoComponent,
    canActivate: [EnterpriseUserGuard]
  },
  {
    path: 'enterprise/departments',
    component: DepartmentComponent,
    canActivate: [EnterpriseUserGuard]
  },
  {
    path: 'enterprise/users',
    component: EnterpriseUsersComponent,
    canActivate: [EnterpriseUserGuard]
  },
  {
    path: 'enterprise',
    redirectTo: 'enterprise/users'
  },
  {
    path: 'invite',
    component: InviteComponent,
    canActivate: [NewUserGuard]
  },
  {
    path: 'create-enterprise',
    component: EnterpriseCreateEnterpriseComponent
  },
  { path: 'start', component: StartComponent, canActivate: [NewUserGuard] },
  { path: '', redirectTo: '/start', pathMatch: 'full' },
  { path: '**', redirectTo: '/start' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
