import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Task, TaskStatus } from 'src/app/models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  constructor(private client: HttpClient) {}

  getTasksByProjectId(projectId: number): Observable<Task[]> {
    const tasks: Task[] = [
      {
        id: 1,
        title: 'Load testing',
        description: 'Task 1 description',
        projectId: 1,
        assignedUsers: ['Mykyta'],
        status: TaskStatus.Todo
      },
      {
        id: 1,
        title: 'Update documentation',
        description: 'Task 1 description',
        projectId: 1,
        assignedUsers: [],
        status: TaskStatus.Todo
      },
      {
        id: 2,
        title: 'Create system',
        description: 'Task 2 description',
        projectId: 1,
        assignedUsers: ['Oleksii', 'Mykyta'],
        status: TaskStatus.Done
      },
      {
        id: 3,
        title: 'Test usability',
        description: 'Task 1 description',
        projectId: 1,
        assignedUsers: ['Oleksii'],
        status: TaskStatus.Commited
      }
    ];

    return of(tasks);
  }
}
