import { TaskStatus } from 'src/app/pages/task/models/task.model';

export interface TaskStatusViewModel {
  status: TaskStatus;
  color: string;
  text: string;
}
