import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faClock, faPencil, faPlus } from '@fortawesome/free-solid-svg-icons';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { map, Observable } from 'rxjs';
import { Task, TaskStatus } from 'src/app/models/task.model';
import { TaskService } from 'src/app/services/task.service';
import { TaskFacade } from 'src/app/store/facades/task.facade';
import { TaskAssignModalComponent } from './task-assign-modal/task-assign-modal.component';
import { TaskCreateModalComponent } from './task-create-modal/task-create-modal.component';
import { TaskDetailsModalComponent } from './task-details-modal/task-details-modal.component';
import { TaskTrackModalComponent } from './task-track-modal/task-track-modal.component';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent implements OnInit {
  plus = faPlus;
  pencil = faPencil;
  clock = faClock;

  tasks$ = this.facade.tasks$;

  constructor(
    private facade: TaskFacade,
    private modalService: NgbModal,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const projectId = +this.route.snapshot.paramMap.get('id')!;
    this.facade.loadTasksByProjectId(projectId);
  }

  $tasksByStatus(statusAsString: string): Observable<Task[]> {
    const status: TaskStatus =
      TaskStatus[statusAsString as keyof typeof TaskStatus];
    return this.tasks$.pipe(
      map((tasks) => tasks.filter((task) => task.status === status))
    );
  }

  openTaskCreateModal(): void {
    const modalRef = this.modalService.open(TaskCreateModalComponent);
    modalRef.componentInstance.projectId =
      +this.route.snapshot.paramMap.get('id')!;
  }

  openTaskAssignModal(taskId: number): void {
    const modalRef = this.modalService.open(TaskAssignModalComponent);
    console.log('taskId', taskId);

    modalRef.componentInstance.taskId = taskId;
  }

  openTaskTrackModal(taskId: number): void {
    const modalRef = this.modalService.open(TaskTrackModalComponent);
    modalRef.componentInstance.taskId = taskId;
  }

  openTaskDetailsModal(task: Task): void {
    const modalRef = this.modalService.open(TaskDetailsModalComponent);
    modalRef.componentInstance.task = task;
  }
}
