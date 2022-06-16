import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Department } from 'src/app/models/department.model';
import { User } from 'src/app/models/user.model';
import { AvatarService } from 'src/app/services/avatar.service';
import { DepartmentService } from 'src/app/services/department.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss']
})
export class DepartmentComponent {
  $departments: Observable<Department[]> = this.service.getDepartments();

  constructor(
    private service: DepartmentService,
    public avatarService: AvatarService
  ) {}
}
