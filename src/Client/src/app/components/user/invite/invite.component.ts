import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { Invite } from 'src/app/models/invite.model';
import { API_URL } from 'src/app/util/urls';
import { InviteAcceptModalComponent } from '../invite-accept-modal/invite-accept-modal.component';

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.scss']
})
export class InviteComponent {
  constructor(private client: HttpClient, private modalService: NgbModal) {}

  $invites: Observable<Invite[]> = this.client.get<Invite[]>(
    API_URL + 'user/enterprise-invites'
  );

  openAcceptModal(invite: Invite): void {
    const modalRef = this.modalService.open(InviteAcceptModalComponent);
    modalRef.componentInstance.invite = invite;
  }
}
