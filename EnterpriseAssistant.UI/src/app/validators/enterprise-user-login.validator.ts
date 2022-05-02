import { HttpClient, HttpParams } from '@angular/common/http';
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

const URL = 'https://localhost:5002/api/enterprise/user/exists';

@Injectable({ providedIn: 'root' })
export class EnterpriseUserLoginValidator implements AsyncValidator {
  constructor(private client: HttpClient) {}

  validate(control: AbstractControl): Observable<ValidationErrors | null> {
    return control.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      switchMap((value) => this.isUserExists(value)),
      map((exists) => {
        if (!exists) {
          return null;
        }
        return { userExists: 'User with provided login already exists' };
      }),
      first()
    );
  }

  private isUserExists(login: string): Observable<boolean> {
    const params = new HttpParams().append('login', login);
    return this.client.get<boolean>(URL, { params: params });
  }
}
