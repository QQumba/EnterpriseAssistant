export interface AppUser {
  userId: number;
  name: string;
  email: string;
  enterpriseIds?: string[];
  enterpriseId?: string;
}
