import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { faCircleExclamation } from '@fortawesome/free-solid-svg-icons';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { DepartmentService } from 'src/app/services/department.service';
import { TaskFacade } from 'src/app/store/facades/task.facade';
import { FormHelper } from 'src/app/util/form-helper';

@Component({
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
  styleUrls: ['./add-user-modal.component.scss']
})
export class AddUserModalComponent implements OnInit, OnDestroy {
  @Input() departmentId!: number;
  warning = faCircleExclamation;

  private subsciption?: Subscription;
  public helper: FormHelper;

  assigneeForm = new FormGroup({
    login: new FormControl('', [Validators.required])
  });

  constructor(
    private service: DepartmentService,
    public activeModal: NgbActiveModal
  ) {
    this.helper = new FormHelper(this.assigneeForm);
  }
  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }

  ngOnInit(): void {}

  submit(): void {
    if (!this.assigneeForm.valid) {
      return;
    }

    const assignee = this.assigneeForm.value;
    this.subsciption = this.service
      .addUserToDepartment(this.departmentId, assignee)
      .subscribe();
    this.activeModal.close();
  }
}
