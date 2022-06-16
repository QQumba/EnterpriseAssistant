import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-project-users',
  templateUrl: './project-users.component.html',
  styleUrls: ['./project-users.component.scss']
})
export class ProjectUsersComponent implements OnInit {
  projectId = +this.route.snapshot.paramMap.get('id')!;

  $users = this.service.getProjectUsers(this.projectId);

  constructor(private service: ProjectService, private route: ActivatedRoute) {}

  ngOnInit(): void {}

  openAddUserModal(): void {}
}
