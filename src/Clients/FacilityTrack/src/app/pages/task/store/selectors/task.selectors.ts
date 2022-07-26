import { createFeatureSelector, createSelector } from '@ngrx/store';
import { taskListAdapter, TaskListState } from '../reducers/task.reducer';

export const tasksFeatureKey = 'tasks';

const { selectEntities, selectAll } = taskListAdapter.getSelectors();

export const selectTaskListState =
  createFeatureSelector<TaskListState>(tasksFeatureKey);

export const selectTasks = createSelector(selectTaskListState, selectAll);
