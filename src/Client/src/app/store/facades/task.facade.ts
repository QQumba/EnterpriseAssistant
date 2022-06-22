import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { TaskCreate } from 'src/app/models/task.model';
import { Assignee } from 'src/app/models/user.model';
import {
  addUserToDepartment,
  assignUserToTask,
  createTask,
  disassignUserFromTask,
  loadTasksByProjectId,
  loadUserTasks
} from '../actions/task.actions';
import { selectTasks } from '../selectors/tasks.selector';

@Injectable({ providedIn: 'root' })
export class TaskFacade {
  tasks$ = this.store.select(selectTasks);

  constructor(private store: Store) {}

  loadTasksByProjectId(projectId: number): void {
    this.store.dispatch(loadTasksByProjectId({ projectId }));
  }

  loadUserTasks(): void {
    this.store.dispatch(loadUserTasks());
  }

  assignUserToTask(taskId: number, user: Assignee): void {
    this.store.dispatch(assignUserToTask({ taskId, user }));
  }

  disassignUserFromTask(taskId: number, userId: number): void {
    this.store.dispatch(disassignUserFromTask({ taskId, userId }));
  }

  createTask(task: TaskCreate): void {
    this.store.dispatch(createTask({ task }));
  }

  addUserToDepartment(departmentId: number, user: Assignee): void {
    this.store.dispatch(addUserToDepartment({ departmentId, user }));
  }
}
