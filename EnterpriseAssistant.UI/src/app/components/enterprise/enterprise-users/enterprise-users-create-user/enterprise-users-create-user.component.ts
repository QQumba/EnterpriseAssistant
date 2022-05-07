import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { faCircleExclamation } from '@fortawesome/free-solid-svg-icons';
import { first } from 'rxjs';
import { UserCreate } from 'src/app/models/user-create.model';
import { User } from 'src/app/models/user.model';
import { EnterpriseService } from 'src/app/services/enterprise.service';
import { EnterpriseUserLoginValidator } from 'src/app/validators/enterprise-user-login.validator';

@Component({
  selector: 'app-enterprise-users-create-user',
  templateUrl: './enterprise-users-create-user.component.html',
  styleUrls: ['./enterprise-users-create-user.component.scss']
})
export class EnterpriseUsersCreateUserComponent {
  // icons
  warning = faCircleExclamation;
  isSubmited = false;

  userFormGroup = new FormGroup({
    login: new FormControl(
      '',
      [Validators.required],
      [
        this.enterpriseUserLoginValidator.validate.bind(
          this.enterpriseUserLoginValidator
        )
      ]
    ),
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [])
  });

  createUserForm = new FormGroup({
    user: this.userFormGroup
  });

  constructor(
    private service: EnterpriseService,
    private enterpriseUserLoginValidator: EnterpriseUserLoginValidator
  ) {}

  isValid(name: string): boolean | undefined {
    const control = this.createUserForm.get(name);
    if (!control) {
      return false;
    }
    return control.untouched || control.valid;
  }

  hasError(name: string): boolean {
    const control = this.createUserForm.get(name);
    if (!control) {
      return false;
    }
    return (
      (control.touched && control.invalid) ||
      (control.dirty && control.invalid) ||
      (this.isSubmited && control.invalid)
    );
  }

  getValidationError(controlName: string, errorType: string): string | null {
    const control = this.createUserForm.get(controlName);
    return control?.errors?.[errorType] as string;
  }

  createUser(): void {
    this.isSubmited = true;
    if (this.userFormGroup.valid) {
      const userCreate = this.userFormGroup.value as UserCreate;
      userCreate.password = 'qwe';
      this.service
        .addUser(userCreate)
        .pipe(first())
        .subscribe((user: User) => {
          console.log(user);
          this.userFormGroup.reset();
          this.isSubmited = false;
        });
    }
  }
}
