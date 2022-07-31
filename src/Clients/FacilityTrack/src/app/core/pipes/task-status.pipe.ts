import { Pipe, PipeTransform } from '@angular/core';
import { TaskStatus } from 'src/app/pages/task/models/task.model';

@Pipe({
  name: 'taskStatus'
})
export class TaskStatusPipe implements PipeTransform {
  transform(value: TaskStatus): unknown {
    return null;
  }
}
