import { createReducer, on } from '@ngrx/store';
import { Task } from 'src/app/models/task.model';
import {
  assignUserToTaskSuccess,
  createTaskSuccess,
  disassignUserFromTask,
  loadTasksSuccess
} from '../actions/task.actions';

export const taskInitialState: Task[] = [];

export const taskReducer = createReducer(
  taskInitialState,
  on(loadTasksSuccess, (_, { tasks }) => tasks),
  on(assignUserToTaskSuccess, (state, { taskId, user }) => {
    const newState: Task[] = Object.assign({}, state);
    const task = newState.find((task) => task.id === taskId);
    if (task) {
      task.assignedUsers = task.assignedUsers || [];
      task.assignedUsers.push(user);
    }
    return newState;
  }),
  on(disassignUserFromTask, (state, { taskId, userId }) => {
    const newState: Task[] = Object.assign({}, state);
    const task = newState.find((task) => task.id === taskId);
    if (task) {
      task.assignedUsers = task.assignedUsers || [];
      task.assignedUsers = task.assignedUsers.filter(
        (user) => user.id !== userId
      );
    }
    return newState;
  }),
  on(createTaskSuccess, (state, { task }) => [...state, task])
);
