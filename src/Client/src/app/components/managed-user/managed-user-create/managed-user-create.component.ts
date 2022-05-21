import { Component } from '@angular/core';
import { faAt, faLock } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-managed-user-create',
  templateUrl: './managed-user-create.component.html',
  styleUrls: ['./managed-user-create.component.scss']
})
export class ManagedUserCreateComponent {
  at = faAt;
  lock = faLock;
}
