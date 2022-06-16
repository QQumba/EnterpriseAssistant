import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Project } from 'src/app/models/project.model';
import { User } from 'src/app/models/user.model';
import { AvatarService } from 'src/app/services/avatar.service';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent {
  $projects: Observable<Project[]> = this.service.getEnterpriseProjects();
  $projectMembers: Observable<User[]> = this.service.getProjectMembers();
  $projectLeads: Observable<User[]> = this.service.getProjectLeads();

  constructor(
    private service: ProjectService,
    public avatarService: AvatarService
  ) {}

  openCreateProjectModal(): void {}

  getUserName(user: User): string {
    let name = user.firstName;
    if (user.lastName) {
      name += ' ' + user.lastName;
    }
    return name;
  }
}
