import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { DepartmentService } from 'src/app/services/department.service';

@Component({
  selector: 'app-department-members',
  templateUrl: './department-members.component.html',
  styleUrls: ['./department-members.component.scss']
})
export class DepartmentMembersComponent implements OnInit {
  @Input() departmentId!: number;

  members$?: Observable<User[]>;

  constructor(public service: DepartmentService) {}

  ngOnInit(): void {
    this.members$ = this.service.getDepartmentUsers(this.departmentId);
  }
}
