import { Component, Input } from '@angular/core';
import { IconProp } from '@fortawesome/fontawesome-svg-core';

@Component({
  selector: 'app-sidebar-menu-action',
  templateUrl: './sidebar-menu-action.component.html',
  styleUrls: ['./sidebar-menu-action.component.scss']
})
export class SidebarMenuActionComponent {
  @Input() icon!: IconProp;
  @Input() text!: string;
  @Input() href!: string;
}
