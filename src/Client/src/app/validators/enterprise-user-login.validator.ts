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
import { API_URL } from '../util/urls';

@Injectable({ providedIn: 'root' })
export class EnterpriseUserLoginValidator implements AsyncValidator {
  constructor(private client: HttpClient) {}

  validate(control: AbstractControl): Observable<ValidationErrors | null> {
    return control.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      switchMap((value) => this.isUserExists(value)),
      map((exists) => {
        if (exists) {
          return { userExists: 'User with provided login already exists' };
        }
        return null;
      }),
      first()
    );
  }

  private isUserExists(login: string): Observable<boolean> {
    const params = new HttpParams().append('login', login);
    return this.client.get<boolean>(`${API_URL}enterprise/${1}/user/exists`, {
      params: params
    });
  }
}
