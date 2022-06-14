import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { faCircleExclamation } from '@fortawesome/free-solid-svg-icons';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription, take } from 'rxjs';
import { SubscriptionLog } from 'rxjs/internal/testing/SubscriptionLog';
import { Enterprise } from 'src/app/models/enterprise.model';
import { InviteCreate } from 'src/app/models/invite.model';
import { API_URL } from 'src/app/util/urls';
import { EnterpriseUserValidatorService } from 'src/app/validators/services/enterprise-user-validator.service';

@Component({
  selector: 'app-invite-user-modal',
  templateUrl: './invite-user-modal.component.html',
  styleUrls: ['./invite-user-modal.component.scss']
})
export class InviteUserModalComponent implements OnDestroy {
  warning = faCircleExclamation;
  private createInviteSubscription?: Subscription;

  constructor(
    private cleint: HttpClient,
    public activeModal: NgbActiveModal,
    private validationService: EnterpriseUserValidatorService
  ) {}

  ngOnDestroy(): void {
    this.createInviteSubscription?.unsubscribe();
  }

  inviteForm = new FormGroup({
    email: new FormControl(
      '',
      [Validators.required, Validators.email],
      [this.validationService.existingEmailValidator()]
    )
  });

  submitted = false;

  submit() {
    this.submitted = true;
    if (!this.inviteForm.valid) {
      return;
    }

    const invite: InviteCreate = {
      userEmail: this.inviteForm.get('email')?.value
    };

    this.createInviteSubscription = this.cleint
      .post<Enterprise>(`${API_URL}enterprise/invite`, invite)
      .pipe(take(1))
      .subscribe(() => {
        this.activeModal.close();
      });
  }

  hasError(name: string): boolean {
    const control = this.inviteForm.get(name);
    if (!control) {
      return false;
    }
    return (
      (control.touched || control.dirty || this.submitted) && control.invalid
    );
  }

  getValidationError(controlName: string, errorType: string): string | null {
    const control = this.inviteForm.get(controlName);

    return control?.errors?.[errorType] as string;
  }
}
