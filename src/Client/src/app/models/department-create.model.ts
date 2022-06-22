export interface DepartmentCreate {
  name: string;
  code: string;
  parentDepartmentId?: number;
  admins?: DepartmentAdmin[];
  doNotJoin: boolean;
  displayAsMember: boolean;
}

export interface DepartmentAdmin {
  id: number;
  displayAsMember: boolean;
}
