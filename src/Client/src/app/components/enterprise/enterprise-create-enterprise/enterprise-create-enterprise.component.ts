import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  faCircleExclamation,
  faCircleNotch,
  faFile
} from '@fortawesome/free-solid-svg-icons';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { map, first } from 'rxjs';
import { EnterpriseCreate } from 'src/app/models/enterprise-create.model';
import { Enterprise } from 'src/app/models/enterprise.model';
import { API_URL } from 'src/app/util/urls';
import { EnterpriseIdValidator } from 'src/app/validators/enterprise-id.validator';

@Component({
  selector: 'app-enterprise-create-enterprise',
  templateUrl: './enterprise-create-enterprise.component.html',
  styleUrls: ['./enterprise-create-enterprise.component.scss']
})
export class EnterpriseCreateEnterpriseComponent implements OnInit {
  // icons
  placeholder = faFile;
  warning = faCircleExclamation;
  spinner = faCircleNotch;

  constructor(
    private idValidator: EnterpriseIdValidator,
    private cleint: HttpClient,
    private authService: OidcSecurityService
  ) {}

  enterpriseFromGroup = new FormGroup({
    id: new FormControl(
      '',
      [Validators.required],
      [this.idValidator.validate.bind(this.idValidator)]
    ),
    displayedName: new FormControl('', [Validators.required]),
    userLogin: new FormControl('', [Validators.required])
  });

  departmentFormGroup = new FormGroup({
    name: new FormControl('', [Validators.required])
  });

  submitted = false;
  createEnterpriseForm = new FormGroup({
    enterprise: this.enterpriseFromGroup,
    department: this.departmentFormGroup
  });

  ngOnInit(): void {
    return;
  }

  submit() {
    this.submitted = true;
    if (!this.createEnterpriseForm.valid) {
      return;
    }

    const enterpriseCreate: EnterpriseCreate = {
      id: this.enterpriseFromGroup.get('id')?.value,
      displayedName: this.enterpriseFromGroup.get('displayedName')?.value,
      userLogin: this.enterpriseFromGroup.get('userLogin')?.value,
      departmentCreate: {
        name: this.departmentFormGroup.get('name')?.value
      }
    };

    this.cleint
      .post<Enterprise>(`${API_URL}enterprise`, enterpriseCreate)
      .pipe(
        map((e) => !!e),
        first()
      )
      .subscribe((success) => {
        if (success) {
          this.authService.authorize();
        }
      });
  }

  hasError(name: string): boolean {
    const control = this.createEnterpriseForm.get(name);
    if (!control) {
      return false;
    }
    return (
      (control.touched || control.dirty || this.submitted) && control.invalid
    );
  }

  getValidationError(controlName: string, errorType: string): string | null {
    const control = this.createEnterpriseForm.get(controlName);

    return control?.errors?.[errorType] as string;
  }
}
