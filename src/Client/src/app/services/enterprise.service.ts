import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Invite } from '../models/invite.model';
import { UserCreate } from '../models/user-create.model';
import { User } from '../models/user.model';
import { API_URL } from '../util/urls';

// todo: move to config
const URL = API_URL + 'enterprise/';

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

  getEneterpriseUsers(): Observable<User[]> {
    return this.client.get<User[]>(URL + 'user');
  }

  getInivtes(): Observable<Invite[]> {
    return this.client.get<Invite[]>(URL + 'invite');
  }
}
