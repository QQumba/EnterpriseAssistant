import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { API_URL } from '../util/urls';

const URL = API_URL + 'user/';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private client: HttpClient) {}

  getUserDetails(): Observable<User> {
    return this.client.get<User>(URL + 'details');
  }
}
