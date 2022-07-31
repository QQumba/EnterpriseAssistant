import { TaskStatus } from 'src/app/pages/task/models/task.model';
import { TaskStatusViewModel } from '../models/task-status.view-model';

const statusNone: TaskStatusViewModel = {
  status: TaskStatus.None,
  color: 'rgb(0, 128, 128)',
  text: 'None'
};

const taskStatusMap: Map<TaskStatus, TaskStatusViewModel> = new Map<
  TaskStatus,
  TaskStatusViewModel
>([
  [TaskStatus.None, statusNone],
  [
    TaskStatus.Commited,
    {
      status: TaskStatus.Commited,
      color: 'rgb(256, 0, 128)',
      text: 'Commited'
    }
  ],
  [
    TaskStatus.InProgress,
    {
      status: TaskStatus.InProgress,
      color: 'rgb(128, 256, 0)',
      text: 'In Progress'
    }
  ],
  [
    TaskStatus.Completed,
    {
      status: TaskStatus.Completed,
      color: 'rgb(0, 0, 256)',
      text: 'Completed'
    }
  ],
  [
    TaskStatus.Rejected,
    {
      status: TaskStatus.Rejected,
      color: 'rgb(0, 128, 0)',
      text: 'Rejected'
    }
  ]
]);

export function transformTaskStatus(status: TaskStatus): TaskStatusViewModel {
  const transformedStatus = taskStatusMap.get(status);
  if (!transformTaskStatus) {
    return statusNone;
  }
  return transformedStatus as TaskStatusViewModel;
}
