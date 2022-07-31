import { createAction, props } from '@ngrx/store';
import { Task } from '../../models/task.model';

export const loadTasks = createAction('[Task] Load Tasks');

export const loadTasksSuccess = createAction(
  '[Task] Load Tasks Success',
  props<{ tasks: Task[] }>()
);

export const loadTasksError = createAction(
  '[Task] Load Tasks Error',
  props<{ error: unknown }>()
);
