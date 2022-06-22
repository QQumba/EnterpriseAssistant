import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Task, TaskCreate, TaskStatus } from 'src/app/models/task.model';
import { Assignee, User } from '../models/user.model';
import { API_URL } from '../util/urls';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  constructor(private client: HttpClient) {}

  getTasksByProjectId(projectId: number): Observable<Task[]> {
    return this.client.get<Task[]>(API_URL + 'task/by-project/' + projectId);
  }

  createTask(task: TaskCreate): Observable<Task> {
    return this.client.post<Task>(API_URL + 'task', task);
  }

  getUserTasks(): Observable<Task[]> {
    return of([]);
  }

  assignUserToTasks(taskId: number, user: Assignee): Observable<User> {
    return this.client.put<User>(API_URL + `task/${taskId}/assign`, user);
  }

  getTaskUsers(taskId: number): Observable<User[]> {
    return this.client.get<User[]>(API_URL + `task/${taskId}/users`);
  }
}
