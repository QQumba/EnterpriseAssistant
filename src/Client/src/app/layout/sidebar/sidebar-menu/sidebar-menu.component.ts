import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import {
  faBuilding,
  faList,
  faProjectDiagram,
  faUser,
  faUserPlus,
  faUsers,
  faWallet
} from '@fortawesome/free-solid-svg-icons';
import { filter, Subscription } from 'rxjs';
import { SidebarMenu } from 'src/app/models/helpers/sidebar-menu.model';

const startMenu: SidebarMenu = {
  title: 'start.sidebar.title',
  actions: [
    { route: '#', text: 'start.sidebar.create-enterprise', icon: faBuilding },
    { route: '#', text: 'start.sidebar.invites', icon: faUserPlus }
  ]
};

const enterpriseMenu: SidebarMenu = {
  title: 'enterprise.sidebar.title',
  actions: [
    { route: '#', text: 'enterprise.sidebar.departments', icon: faUsers },
    { route: '#', text: 'enterprise.sidebar.projects', icon: faProjectDiagram },
    { route: '#', text: 'enterprise.sidebar.users', icon: faUser },
    { route: '#', text: 'enterprise.sidebar.billings', icon: faWallet }
  ]
};

const depatmentMenu: SidebarMenu = {
  title: 'department.sidebar.title',
  actions: [
    { route: '#', text: 'department.sidebar.overview', icon: faList },
    { route: '#', text: 'department.sidebar.users', icon: faUser }
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
      if (e instanceof NavigationStart) {
        console.log(e);
      }
    });
  }
  ngOnDestroy(): void {
    this.routerSubscription?.unsubscribe();
  }
}
