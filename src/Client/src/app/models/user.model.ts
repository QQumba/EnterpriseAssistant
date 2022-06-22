export interface User {
  id: number;
  email: string;
  login?: string;
  firstName: string;
  lastName?: string;
}

export interface Assignee {
  login: string;
}
