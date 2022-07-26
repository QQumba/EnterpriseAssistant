import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { Task, TaskStatus } from '../models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  constructor(private client: HttpClient) {}

  public getTasksByProjectId(projectId: number): Observable<Task[]> {
    const tasks: Task[] = [
      {
        taskId: 1,
        title: 'Task 1',
        description: 'Task 1 description',
        createdDateTime: new Date(),
        status: TaskStatus.Commited,
        effortHours: 1
      },
      {
        taskId: 2,
        title: 'Task 2',
        description: 'Task 2 description',
        createdDateTime: new Date(),
        status: TaskStatus.InProgress
      },
      {
        taskId: 3,
        title: 'Task 3',
        description: 'Task 3 description',
        createdDateTime: new Date(),
        status: TaskStatus.Completed,
        effortHours: 2
      }
    ];

    return of(tasks);
    return this.client.get<Task[]>('/api/tasks');
  }
}
