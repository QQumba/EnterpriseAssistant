import { Component, OnInit } from '@angular/core';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { map, Observable } from 'rxjs';
import { Task, TaskStatus } from 'src/app/models/task.model';
import { TaskService } from 'src/app/services/task.service';
import { TaskDetailsModalComponent } from './task-details-modal/task-details-modal.component';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent {
  plus = faPlus;

  $tasks = this.service.getTasksByProjectId(1);

  constructor(private service: TaskService, private modalService: NgbModal) {}

  $tasksByStatus(statusAsString: string): Observable<Task[]> {
    const status: TaskStatus =
      TaskStatus[statusAsString as keyof typeof TaskStatus];
    return this.$tasks.pipe(
      map((tasks) => tasks.filter((task) => task.status === status))
    );
  }

  openDetailsModal(task: Task): void {
    const modalRef = this.modalService.open(TaskDetailsModalComponent);
    modalRef.componentInstance.task = task;
  }
}
