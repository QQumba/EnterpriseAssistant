import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { AvatarService } from 'src/app/services/avatar.service';
import { DepartmentService } from 'src/app/services/department.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.scss']
})
export class MembersComponent implements OnInit {
  @Input() departmentId!: number;

  $members?: Observable<User[]>;

  constructor(
    public service: DepartmentService,
    public avatarService: AvatarService
  ) {}

  ngOnInit(): void {
    this.$members = this.service.getDepartmentUsers(this.departmentId);
  }

  getUserName(user: User): string {
    let name = user.firstName;
    if (user.lastName) {
      name += ' ' + user.lastName;
    }
    return name;
  }
}
