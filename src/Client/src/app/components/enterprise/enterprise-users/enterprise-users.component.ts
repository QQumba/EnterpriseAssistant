import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterpriseService } from 'src/app/services/enterprise.service';
import { InviteUserModalComponent } from '../invite-user-modal/invite-user-modal.component';

@Component({
  selector: 'app-enterprise-users',
  templateUrl: './enterprise-users.component.html',
  styleUrls: ['./enterprise-users.component.scss']
})
export class EnterpriseUsersComponent {
  $users = this.service.getEneterpriseUsers();

  constructor(
    private service: EnterpriseService,
    private modalService: NgbModal
  ) {}

  openInviteModal(): void {
    this.modalService.open(InviteUserModalComponent);
  }
}
