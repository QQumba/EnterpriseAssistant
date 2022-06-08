import { Component } from '@angular/core';
import {
  faPlus,
  faProjectDiagram,
  faUser,
  faUsers,
  faWallet
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {
  plus = faPlus;

  department = faUsers;
  project = faProjectDiagram;
  user = faUser;
  billing = faWallet;

  
}
