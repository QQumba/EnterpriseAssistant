import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserCreate } from '../models/user-create.model';
import { User } from '../models/user.model';

// todo: move to config
const URL = 'https://localhost:5002/api/enterprise/';

@Injectable({
  providedIn: 'root'
})
export class EnterpriseService {
  constructor(private client: HttpClient) {}

  addUser(userCreate: UserCreate): Observable<User> {
    return this.client.post<User>(URL + 'user', userCreate);
  }

  isUserExists(login: string): Observable<boolean> {
    const params = new HttpParams().append('login', login);
    return this.client.get<boolean>(URL + 'user/exists', {
      params: params
    });
  }
}
