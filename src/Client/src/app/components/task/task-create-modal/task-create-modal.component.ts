import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TaskFacade } from 'src/app/store/facades/task.facade';

@Component({
  selector: 'app-task-create-modal',
  templateUrl: './task-create-modal.component.html',
  styleUrls: ['./task-create-modal.component.scss']
})
export class TaskCreateModalComponent implements OnInit {
  @Input() projectId!: number;

  taskCreateForm = new FormGroup({
    title: new FormControl('', [Validators.required]),
    description: new FormControl('')
  });

  constructor(private facade: TaskFacade, public activeModal: NgbActiveModal) {}

  ngOnInit(): void {}

  save(): void {
    if (!this.taskCreateForm.valid) {
      return;
    }
    const task = this.taskCreateForm.value;
    task.projectId = this.projectId;
    this.facade.createTask(task);
    this.activeModal.close();
  }
}
