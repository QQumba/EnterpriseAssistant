import { Component } from '@angular/core';
import { EnterpriseService } from 'src/app/services/enterprise.service';

@Component({
  selector: 'app-enterprise-users',
  templateUrl: './enterprise-users.component.html',
  styleUrls: ['./enterprise-users.component.scss']
})
export class EnterpriseUsersComponent {
  constructor(private service: EnterpriseService) {}
}
