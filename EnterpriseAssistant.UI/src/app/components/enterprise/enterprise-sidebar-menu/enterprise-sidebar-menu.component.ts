import { Component } from '@angular/core';
import {
  faProjectDiagram,
  faUser,
  faUsers,
  faWallet
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-enterprise-sidebar-menu',
  templateUrl: './enterprise-sidebar-menu.component.html',
  styleUrls: ['./enterprise-sidebar-menu.component.scss']
})
export class EnterpriseSidebarMenuComponent {
  department = faUsers;
  project = faProjectDiagram;
  user = faUser;
  billing = faWallet;
}
