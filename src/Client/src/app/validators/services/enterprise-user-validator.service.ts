import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  AbstractControl,
  AsyncValidatorFn,
  ValidationErrors
} from '@angular/forms';
import {
  Observable,
  debounceTime,
  distinctUntilChanged,
  switchMap,
  map,
  first
} from 'rxjs';
import { API_URL } from 'src/app/util/urls';

@Injectable({
  providedIn: 'root'
})
export class EnterpriseUserValidatorService {
  constructor(private client: HttpClient) {}

  loginValidator(enterpriseId: string): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      return control.valueChanges.pipe(
        debounceTime(400),
        distinctUntilChanged(),
        switchMap((login) => this.isUserExists(login, enterpriseId)),
        map((exists) => {
          if (exists) {
            return { error: 'User with provided login already exists' };
          }
          return null;
        }),
        first()
      );
    };
  }

  private isUserExists(
    login: string,
    enterpriseId: string
  ): Observable<boolean> {
    const params = new HttpParams().append('login', login);
    return this.client.get<boolean>(
      `${API_URL}enterprise/${enterpriseId}/user/exists`,
      {
        params: params
      }
    );
  }
}
