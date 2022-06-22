import { createAction, props } from '@ngrx/store';
import { Task, TaskCreate } from 'src/app/models/task.model';
import { Assignee, User } from 'src/app/models/user.model';

export const loadTasksByProjectId = createAction(
  '[Task] Load Tasks',
  props<{ projectId: number }>()
);

export const loadUserTasks = createAction('[Task] Load User Tasks');

export const loadTasksSuccess = createAction(
  '[Task] Load Tasks Success',
  props<{ tasks: Task[] }>()
);

export const loadTasksFailure = createAction(
  '[Task] Load Tasks Failure',
  props<{ error: unknown }>()
);

export const assignUserToTask = createAction(
  '[Task] Assign User To Task',
  props<{ taskId: number; user: Assignee }>()
);

export const assignUserToTaskSuccess = createAction(
  '[Task] Assign User To Task Success',
  props<{ taskId: number; user: User }>()
);

export const disassignUserFromTask = createAction(
  '[Task] Disassign User From Task',
  props<{ taskId: number; userId: number }>()
);

export const disassignUserFromTaskSuccess = createAction(
  '[Task] Disassign User From Task Success',
  props<{ taskId: number; userId: number }>()
);

export const createTask = createAction(
  '[Task] Create Task',
  props<{ task: TaskCreate }>()
);

export const createTaskSuccess = createAction(
  '[Task] Create Task Success',
  props<{ task: Task }>()
);

export const addUserToDepartment = createAction(
  '[Department] Add User To Department',
  props<{ departmentId: number; user: Assignee }>()
);
