import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Project } from '../models/project.model';
import { User } from '../models/user.model';
import { API_URL } from '../util/urls';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  constructor(private client: HttpClient) {}

  getEnterpriseProjects(): Observable<Project[]> {
    const projects: Project[] = [
      {
        id: 1,
        name: 'Project 1',
        description: 'Project 1 description'
      },
      {
        id: 2,
        name: 'Project 2',
        description: 'Project 2 description'
      }
    ];

    return of(projects);
  }

  getProjectUsers(projectId: number): Observable<User[]> {
    return this.client.get<User[]>(API_URL + 'enterprise/user');
  }
}
