import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  AbstractControl,
  AsyncValidator,
  ValidationErrors
} from '@angular/forms';
import {
  debounceTime,
  distinctUntilChanged,
  first,
  map,
  Observable,
  switchMap
} from 'rxjs';

const URL = 'api/department/exists';

@Injectable({ providedIn: 'root' })
export class DepartmentCodeValidator implements AsyncValidator {
  constructor(private client: HttpClient) {}

  validate(control: AbstractControl): Observable<ValidationErrors | null> {
    return control.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      switchMap((value) => this.isCodeAvailable(value)),
      map((exists) => {
        if (exists) {
          return { error: 'Code is already is use' };
        }
        return null;
      }),
      first()
    );
  }

  private isCodeAvailable(code: string): Observable<boolean> {
    return this.client.get<boolean>(URL + '?code=' + code);
  }
}
