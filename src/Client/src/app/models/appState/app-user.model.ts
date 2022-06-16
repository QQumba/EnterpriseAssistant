import { User } from '../user.model';

export interface AppUser {
  enterpriseIds?: string[];
  enterpriseId?: string;
  userDetails: User;
}
