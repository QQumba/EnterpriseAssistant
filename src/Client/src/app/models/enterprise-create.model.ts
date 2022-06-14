import { DepartmentCreate } from './department-create.model';

export interface EnterpriseCreate {
  id: string;
  displayedName: string;
  userLogin: string;
  departmentCreate: DepartmentCreate;
}
