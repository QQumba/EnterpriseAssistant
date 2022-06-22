import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { DepartmentService } from 'src/app/services/department.service';
import { AddUserModalComponent } from '../add-user-modal/add-user-modal.component';

@Component({
  selector: 'app-department-users',
  templateUrl: './department-users.component.html',
  styleUrls: ['./department-users.component.scss']
})
export class DepartmentUsersComponent {
  departmentId = +this.route.snapshot.paramMap.get('id')!;

  constructor(
    private service: DepartmentService,
    private route: ActivatedRoute,
    private modalService: NgbModal
  ) {}

  $users: Observable<User[]> = this.service.getDepartmentUsers(
    this.departmentId
  );

  openInviteModal(): void {
    const modalRef = this.modalService.open(AddUserModalComponent);
    modalRef.componentInstance.departmentId = this.departmentId;
  }
}
