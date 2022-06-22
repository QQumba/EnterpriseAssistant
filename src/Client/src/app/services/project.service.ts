import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Project, ProjectCreate } from '../models/project.model';
import { User } from '../models/user.model';
import { API_URL } from '../util/urls';

const users: User[] = [
  {
    id: 1,
    firstName: 'John',
    lastName: 'Doe',
    email: 'email'
  },
  {
    id: 1,
    firstName: 'Chirs',
    lastName: 'Smith',
    email: 'email'
  }
];

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  constructor(private client: HttpClient) {}

  getEnterpriseProjects(): Observable<Project[]> {
    return this.client.get<Project[]>(API_URL + 'project');
  }

  createProject(projectCreate: ProjectCreate): Observable<Project> {
    return this.client.post<Project>(API_URL + 'project', projectCreate);
  }

  getProjectUsers(projectId: number): Observable<User[]> {
    return this.client.get<User[]>(API_URL + 'enterprise/user');
  }

  getProjectMembers(): Observable<User[]> {
    return of(users);
  }

  getProjectLeads(): Observable<User[]> {
    return of(users);
  }
}
