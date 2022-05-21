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

const URL = 'https://localhost:5002/api/enterprise/isIdAvailable';

@Injectable({ providedIn: 'root' })
export class EnterpriseIdValidator implements AsyncValidator {
  constructor(private client: HttpClient) {}

  validate(control: AbstractControl): Observable<ValidationErrors | null> {
    return control.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      switchMap((value) => this.isIdAvailable(value)),
      map((isAvailable) => {
        if (isAvailable) {
          return null;
        }
        return { error: 'Id is already taken' };
      }),
      first()
    );
  }

  private isIdAvailable(id: string): Observable<boolean> {
    return this.client.get<boolean>(URL + '?id=' + id);
  }
}
