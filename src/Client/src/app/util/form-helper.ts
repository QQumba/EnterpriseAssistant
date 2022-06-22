import { FormGroup } from '@angular/forms';

export class FormHelper {
  private submitted = false;

  constructor(private form: FormGroup) {}

  submit() {
    this.submitted = true;
    if (!this.form.valid) {
      return;
    }
  }

  hasError(name: string): boolean {
    const control = this.form.get(name);
    if (!control) {
      return false;
    }
    return (
      (control.touched || control.dirty || this.submitted) && control.invalid
    );
  }

  getValidationError(controlName: string, errorType: string): string | null {
    const control = this.form.get(controlName);

    return control?.errors?.[errorType] as string;
  }
}
