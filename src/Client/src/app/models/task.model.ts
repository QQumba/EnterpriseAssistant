import { User } from './user.model';

export interface Task {
  id: number;
  title: string;
  description: string;
  projectId: number;
  assignedUsers?: User[];
  status: TaskStatus;
}

export interface TaskCreate {
  title: string;
  description: string;
  projectId: number;
}

export interface TaskUpdateCreate {
  id: number;
  title: string;
  description: string;
  projectId: number;
}

export enum TaskStatus {
  Todo = 'Todo',
  InProgress = 'InProgress',
  Commited = 'Commited',
  Done = 'Done'
}
