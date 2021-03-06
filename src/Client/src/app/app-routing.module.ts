import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DepartmentCreateComponent } from './components/department/department-create/department-create.component';
import { DepartmentInfoComponent } from './components/department/department-info/department-info.component';
import { DepartmentUsersComponent } from './components/department/department-users/department-users.component';
import { DepartmentComponent } from './components/department/department.component';
import { EnterpriseCreateEnterpriseComponent } from './components/enterprise/enterprise-create-enterprise/enterprise-create-enterprise.component';
import { EnterpriseInvitesComponent } from './components/enterprise/enterprise-invites/enterprise-invites.component';
import { EnterpriseUsersComponent } from './components/enterprise/enterprise-users/enterprise-users.component';
import { ProjectCreateComponent } from './components/project/project-create/project-create.component';
import { ProjectUsersComponent } from './components/project/project-users/project-users.component';
import { ProjectComponent } from './components/project/project.component';
import { StartComponent } from './components/start/start.component';
import { TaskComponent } from './components/task/task.component';
import { InviteComponent } from './components/user/invite/invite.component';
import { UserSettingsComponent } from './components/user/user-settings/user-settings.component';
import { EnterpriseUserGuard } from './guards/enterprise-user.guard';
import { NewUserGuard } from './guards/new-user.guard';

const routes: Routes = [
  {
    path: 'project/:id/users',
    component: ProjectUsersComponent,
    canActivate: [EnterpriseUserGuard]
  },
  {
    path: 'project/:id/tasks',
    component: TaskComponent,
    canActivate: [EnterpriseUserGuard]
  },
  {
    path: 'project/:id',
    redirectTo: 'project/:id/tasks'
  },
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
    path: 'enterprise',
    canActivateChild: [EnterpriseUserGuard],
    children: [
      {
        path: 'departments',
        children: [
          {
            path: 'create',
            component: DepartmentCreateComponent
          },
          { path: '**', component: DepartmentComponent }
        ]
      },
      {
        path: 'users',
        component: EnterpriseUsersComponent
      },
      {
        path: 'projects',
        children: [
          {
            path: 'create',
            component: ProjectCreateComponent
          },
          {
            path: '**',
            component: ProjectComponent
          }
        ]
      },
      {
        path: 'invites',
        component: EnterpriseInvitesComponent
      },
      {
        path: '',
        redirectTo: 'users',
        pathMatch: 'full'
      }
    ]
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
