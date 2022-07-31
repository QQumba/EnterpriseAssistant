import { createEntityAdapter, EntityState } from '@ngrx/entity';
import { createReducer, on } from '@ngrx/store';
import { Task } from '../../models/task.model';
import { loadTasksSuccess } from '../actions/task.actions';

export type TaskListState = EntityState<Task>;

export function selectTaskId(task: Task): number {
  return task.taskId;
}

export function sortByCreatedDateTime(a: Task, b: Task): number {
  return b.createdDateTime.getTime() - a.createdDateTime.getTime();
}

export const taskListAdapter = createEntityAdapter<Task>({
  selectId: selectTaskId,
  sortComparer: sortByCreatedDateTime
});

export const taskListInitialState = taskListAdapter.getInitialState();

export const taskListReducer = createReducer(
  taskListInitialState,
  on(loadTasksSuccess, (state, { tasks }) =>
    taskListAdapter.setAll(tasks, state)
  )
);
