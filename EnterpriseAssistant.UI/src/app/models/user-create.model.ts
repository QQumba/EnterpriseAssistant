export interface UserCreate {
  login: string;
  firstName: string;
  lastName?: string;
  password: string;
}
