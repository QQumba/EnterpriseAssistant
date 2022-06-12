import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { DepartmentService } from 'src/app/services/department.service';

@Component({
  selector: 'app-department-users',
  templateUrl: './department-users.component.html',
  styleUrls: ['./department-users.component.scss']
})
export class DepartmentUsersComponent {
  departmentId = +this.route.snapshot.paramMap.get('id')!;

  constructor(
    private service: DepartmentService,
    private route: ActivatedRoute
  ) {}

  $users: Observable<User[]> = this.service.getDepartmentUsers(
    this.departmentId
  );

  openInviteModal(): void {}
}
