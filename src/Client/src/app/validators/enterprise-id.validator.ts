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

const URL = 'api/enterprise/exists';

@Injectable({ providedIn: 'root' })
export class EnterpriseIdValidator implements AsyncValidator {
  constructor(private client: HttpClient) {}

  validate(control: AbstractControl): Observable<ValidationErrors | null> {
    return control.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      switchMap((value) => this.isIdAvailable(value)),
      map((exists) => {
        if (exists) {
          return { error: 'Id is already taken' };
        }
        return null;
      }),
      first()
    );
  }

  private isIdAvailable(id: string): Observable<boolean> {
    return this.client.get<boolean>(URL + '?id=' + id);
  }
}
