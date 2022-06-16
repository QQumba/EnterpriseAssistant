import { DepartmentCreate } from './department-create.model';

export interface Project {
  id: number;
  name: string;
  description: string;
}

export interface ProjectCreate {
  name: string;
  description: string;
  departmentCreate: DepartmentCreate;
}

export interface ProjectUpdate {
  id: number;
  name: string;
  description: string;
}
