import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DepartmentCreate } from '../models/department-create.model';
import { Department } from '../models/department.model';
import { Assignee, User } from '../models/user.model';
import { API_URL } from '../util/urls';

const URL = API_URL + 'department/';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  constructor(private client: HttpClient) {}

  createDepartment(department: DepartmentCreate): Observable<Department> {
    return this.client.post<Department>(URL, department);
  }

  getDepartments(): Observable<Department[]> {
    return this.client.get<Department[]>(URL);
  }

  getDepartmentUsers(departmentId: number): Observable<User[]> {
    return this.client.get<User[]>(`${URL + departmentId}/user`);
  }

  addUserToDepartment(departmentId: number, user: Assignee): Observable<User> {
    return this.client.post<User>(`${URL + departmentId}/user`, user);
  }
}
