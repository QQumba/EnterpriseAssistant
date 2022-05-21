import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { faCircleExclamation } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-form-segment',
  templateUrl: './form-segment.component.html',
  styleUrls: ['./form-segment.component.scss']
})
export class FormSegmentComponent {
  @Input() parentForm!: FormGroup;
  @Input() formSegment!: FormGroup;
  @Input() formSegmentName!: string;

  warning = faCircleExclamation;

  controls = () => Object.keys(this.formSegment.controls);

  hasError(name: string): boolean {
    const control = this.parentForm.get(this.formSegmentName + '.' + name);
    if (!control) {
      return false;
    }
    return (
      (control.touched && control.invalid) || (control.dirty && control.invalid)
    );
  }

  getValidationError(controlName: string, errorType: string): string | null {
    const control = this.parentForm.get(
      this.formSegmentName + '.' + controlName
    );
    return control?.errors?.[errorType] as string;
  }
}
