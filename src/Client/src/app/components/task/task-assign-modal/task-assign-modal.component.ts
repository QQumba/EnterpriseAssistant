import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  faCircleExclamation,
  faWarning
} from '@fortawesome/free-solid-svg-icons';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TaskFacade } from 'src/app/store/facades/task.facade';
import { FormHelper } from 'src/app/util/form-helper';

@Component({
  selector: 'app-task-assign-modal',
  templateUrl: './task-assign-modal.component.html',
  styleUrls: ['./task-assign-modal.component.scss']
})
export class TaskAssignModalComponent implements OnInit {
  @Input() taskId!: number;

  warning = faCircleExclamation;
  public helper: FormHelper;

  assigneeForm = new FormGroup({
    login: new FormControl('', [Validators.required])
  });

  constructor(private facade: TaskFacade, public activeModal: NgbActiveModal) {
    this.helper = new FormHelper(this.assigneeForm);
  }

  ngOnInit(): void {}

  submit(): void {
    if (!this.assigneeForm.valid) {
      return;
    }

    const assignee = this.assigneeForm.value;
    this.facade.assignUserToTask(this.taskId, assignee);
    this.activeModal.close();
  }
}
