import { Component, Input } from '@angular/core';
import {
  faAngleRight,
  faCircle,
  faEllipsisVertical,
  faTrash
} from '@fortawesome/free-solid-svg-icons';
import { transformTaskStatus } from 'src/app/core/helpers/task-status.helper';
import { TaskStatusViewModel } from 'src/app/core/models/task-status.view-model';
import { User } from 'src/app/core/models/user.model';
import { Task, TaskStatus } from '../../models/task.model';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent {
  @Input() task!: Task;

  moreDots = faEllipsisVertical;
  arrow = faAngleRight;
  trash = faTrash;
  circle = faCircle;
  users: User[] = [
    {
      userId: 1,
      firstName: 'John',
      lastName: 'Doe'
    },
    {
      userId: 2,
      firstName: 'Jane',
      lastName: 'Vane'
    },
    {
      userId: 3,
      firstName: 'Sam',
      lastName: 'Doe'
    }
  ];

  transformStatus(status: TaskStatus): TaskStatusViewModel {
    return transformTaskStatus(status);
  }

  onTaskClick(): void {
    console.log('Task clicked');
  }

  onActionClick(event: MouseEvent): void {
    console.log('Action clicked');
    event.stopPropagation();
  }
}
