import { Component, OnInit } from '@angular/core';
import { faAngleUp } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-enterprise-sidebar-enterprise-picker',
  templateUrl: './enterprise-sidebar-enterprise-picker.component.html',
  styleUrls: ['./enterprise-sidebar-enterprise-picker.component.scss']
})
export class EnterpriseSidebarEnterprisePickerComponent {
  arrowUp = faAngleUp;

  isExpanded = false;
}
