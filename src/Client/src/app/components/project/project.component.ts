import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Project } from 'src/app/models/project.model';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent {
  $projects: Observable<Project[]> = this.service.getEnterpriseProjects();

  constructor(private service: ProjectService) {}

  openCreateProjectModal(): void {}
}
