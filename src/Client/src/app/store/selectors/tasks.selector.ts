import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Task } from 'src/app/models/task.model';

export const selectTasks = createFeatureSelector<Task[]>('tasks');

export const selectTaskUsers = createSelector(selectTasks, (tasks: Task[]) => {
  return tasks.map((task) => task.assignedUsers);
});
