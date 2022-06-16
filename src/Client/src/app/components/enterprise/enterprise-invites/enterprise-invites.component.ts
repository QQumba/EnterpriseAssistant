import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Invite } from 'src/app/models/invite.model';
import { EnterpriseService } from 'src/app/services/enterprise.service';

@Component({
  selector: 'app-enterprise-invites',
  templateUrl: './enterprise-invites.component.html',
  styleUrls: ['./enterprise-invites.component.scss']
})
export class EnterpriseInvitesComponent {
  $invites: Observable<Invite[]> = this.service.getInivtes();

  constructor(private service: EnterpriseService) {}
}
