import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { faExclamationCircle } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-form-input',
  templateUrl: './form-input.component.html',
  styleUrls: ['./form-input.component.scss']
})
export class FormInputComponent {
  @Input() parentForm!: FormGroup;
  @Input() controlName!: string;
  @Input() label!: string;
  @Input() placeholder!: string;

  warning = faExclamationCircle;

  hasError(): boolean {
    const control = this.parentForm.get(this.controlName);
    if (!control) {
      return false;
    }
    return (
      (control.touched && control.invalid) || (control.dirty && control.invalid)
    );
  }

  getValidationError(errorType: string): string | null {
    const control = this.parentForm.get(this.controlName);
    return control?.errors?.[errorType] as string;
  }

  toSentenseCase(text: string): string {
    const result = text.replace(/([A-Z])/g, ' $1').toLowerCase();
    const finalResult = result.charAt(0).toUpperCase() + result.slice(1);
    return finalResult;
  }
}
