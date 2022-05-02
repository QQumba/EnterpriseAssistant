import { Component } from '@angular/core';
import { faList, faUser } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-department-sidebar-menu',
  templateUrl: './department-sidebar-menu.component.html',
  styleUrls: ['./department-sidebar-menu.component.scss']
})
export class DepartmentSidebarMenuComponent {
  user = faUser;
  overview = faList;
}
