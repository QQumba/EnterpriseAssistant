import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { faCircleExclamation } from '@fortawesome/free-solid-svg-icons';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { first, map } from 'rxjs';
import { InviteAccept } from 'src/app/models/invite-accept.model';
import { Invite } from 'src/app/models/invite.model';
import { API_URL } from 'src/app/util/urls';
import { EnterpriseUserValidatorService } from 'src/app/validators/services/enterprise-user-validator.service';

@Component({
  selector: 'app-invite-accept-modal',
  templateUrl: './invite-accept-modal.component.html',
  styleUrls: ['./invite-accept-modal.component.scss']
})
export class InviteAcceptModalComponent implements OnInit {
  @Input() invite!: Invite;

  warning = faCircleExclamation;

  acceptInviteForm = new FormGroup({
    login: new FormControl('', [Validators.required])
  });

  constructor(
    public activeModal: NgbActiveModal,
    private client: HttpClient,
    private enterpriseUserValidationService: EnterpriseUserValidatorService,
    private authService: OidcSecurityService
  ) {}

  ngOnInit(): void {
    this.acceptInviteForm
      .get('login')
      ?.addAsyncValidators(
        this.enterpriseUserValidationService.loginValidator(
          this.invite.enterpriseId
        )
      );
  }

  accept(): void {
    if (!this.acceptInviteForm.valid) {
      return;
    }

    console.log('accepting invite');
    const inviteAccept: InviteAccept = {
      enterpriseId: this.invite.enterpriseId,
      login: this.acceptInviteForm.get('login')?.value
    };
    this.client
      .put(`${API_URL}enterprise/invite`, inviteAccept)
      .pipe(
        map(() => true),
        first()
      )
      .subscribe((success) => {
        if (success) {
          this.authService.authorize();
        }
        this.activeModal.close();
      });
  }

  isValid(name: string): boolean | undefined {
    const control = this.acceptInviteForm.get(name);
    if (!control) {
      return false;
    }
    return control.untouched || control.valid;
  }

  hasError(name: string): boolean {
    const control = this.acceptInviteForm.get(name);
    if (!control) {
      return false;
    }
    return (
      (control.touched && control.invalid) || (control.dirty && control.invalid)
    );
  }

  getValidationError(controlName: string, errorType: string): string | null {
    const control = this.acceptInviteForm.get(controlName);

    return control?.errors?.[errorType] as string;
  }
}
