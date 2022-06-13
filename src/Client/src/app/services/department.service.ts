import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Department } from '../models/department.model';
import { User } from '../models/user.model';
import { API_URL } from '../util/urls';

const URL = API_URL + 'department/';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  constructor(private client: HttpClient) {}

  getDepartments(): Observable<Department[]> {
    return this.client.get<Department[]>(URL);
  }

  getDepartmentUsers(departmentId: number): Observable<User[]> {
    return this.client.get<User[]>(`${URL + departmentId}/user`);
  }
}
