import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';
import {
  faBuilding,
  faList,
  faProjectDiagram,
  faUser,
  faUserPlus,
  faUsers,
  faWallet
} from '@fortawesome/free-solid-svg-icons';
import { Subscription } from 'rxjs';
import { SidebarMenu } from 'src/app/models/helpers/sidebar-menu.model';

const startMenu: SidebarMenu = {
  title: 'start.sidebar.title',
  actions: [
    {
      route: 'create-enterprise',
      text: 'start.sidebar.create-enterprise',
      icon: faBuilding
    },
    { route: 'invite', text: 'start.sidebar.invites', icon: faUserPlus }
  ]
};

const enterpriseMenu: SidebarMenu = {
  title: 'enterprise.sidebar.title',
  actions: [
    {
      route: '/enterprise/departments',
      text: 'enterprise.sidebar.departments',
      icon: faUsers
    },
    {
      route: '/enterprise/projects',
      text: 'enterprise.sidebar.projects',
      icon: faProjectDiagram
    },
    {
      route: '/enterprise/users',
      text: 'enterprise.sidebar.users',
      icon: faUser
    },
    {
      route: '/enterprise/invites',
      text: 'enterprise.sidebar.invites',
      icon: faUserPlus
    }
  ]
};

const depatmentMenu: SidebarMenu = {
  title: 'department.sidebar.title',
  actions: [
    {
      route: '/department/:id',
      text: 'department.sidebar.overview',
      icon: faList
    },
    {
      route: '/department/:id/users',
      text: 'department.sidebar.users',
      icon: faUser
    }
  ]
};

const projectMenu: SidebarMenu = {
  title: 'project.sidebar.title',
  actions: [
    {
      route: '/project/:id/users',
      text: 'project.sidebar.users',
      icon: faUser
    },
    {
      route: '/project/:id/tasks',
      text: 'project.sidebar.tasks',
      icon: faList
    }
  ]
};

@Component({
  selector: 'app-sidebar-menu',
  templateUrl: './sidebar-menu.component.html',
  styleUrls: ['./sidebar-menu.component.scss']
})
export class SidebarMenuComponent implements OnInit, OnDestroy {
  // todo: remove with service provided values
  menu: SidebarMenu = startMenu;

  private routerSubscription?: Subscription;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.routerSubscription = this.router.events.subscribe((e) => {
      if (e instanceof NavigationEnd) {
        console.log(e);
        if (e.url.startsWith('/enterprise')) {
          this.menu = enterpriseMenu;
        }
        if (e.url.startsWith('/department')) {
          const departmentId = e.url.split('/')[2];
          this.setDepartmentMenu(departmentId);
        }
        if (e.url.startsWith('/project')) {
          const projectId = e.url.split('/')[2];
          this.setProjecttMenu(projectId);
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.routerSubscription?.unsubscribe();
  }

  setDepartmentMenu(departmentId: string): void {
    const actions = depatmentMenu.actions.map((a) => {
      const newRoute = a.route.replace(':id', departmentId);
      return { ...a, route: newRoute };
    });
    this.menu = { ...depatmentMenu, actions };
  }

  setProjecttMenu(projectId: string): void {
    const actions = projectMenu.actions.map((a) => {
      const newRoute = a.route.replace(':id', projectId);
      return { ...a, route: newRoute };
    });
    this.menu = { ...projectMenu, actions };
  }
}
