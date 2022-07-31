export interface Task {
  taskId: number;
  title: string;
  description: string;
  createdDateTime: Date;
  status: TaskStatus;
  effortHours?: number;
}

export enum TaskStatus {
  None = 'None',
  Commited = 'Commited',
  InProgress = 'InProgress',
  Completed = 'Completed',
  Rejected = 'Rejected'
}
