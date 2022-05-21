import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  faCircleExclamation,
  faCircleNotch,
  faFile
} from '@fortawesome/free-solid-svg-icons';
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

  constructor(private idValidator: EnterpriseIdValidator) {}

  enterpriseFromGroup = new FormGroup({
    id: new FormControl(
      '',
      [Validators.required],
      [this.idValidator.validate.bind(this.idValidator)]
    ),
    displayedName: new FormControl('', [Validators.required])
  });

  departmentFormGroup = new FormGroup({
    name: new FormControl('', [Validators.required])
  });

  userFormGroup = new FormGroup({
    login: new FormControl('', [Validators.required]),
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl(''),
    password: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required])
  });

  createEnterpriseForm = new FormGroup({
    enterprise: this.enterpriseFromGroup,
    department: this.departmentFormGroup,
    user: this.userFormGroup
  });

  ngOnInit(): void {
    return;
  }

  isValid(name: string): boolean | undefined {
    const control = this.createEnterpriseForm.get(name);
    if (!control) {
      return false;
    }
    return control.untouched || control.valid;
  }

  hasError(name: string): boolean {
    const control = this.createEnterpriseForm.get(name);
    if (!control) {
      return false;
    }
    return (
      (control.touched && control.invalid) || (control.dirty && control.invalid)
    );
  }

  getValidationError(controlName: string, errorType: string): string | null {
    const control = this.createEnterpriseForm.get(controlName);

    return control?.errors?.[errorType] as string;
  }
}
