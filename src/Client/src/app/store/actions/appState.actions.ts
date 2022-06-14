import { createAction, props } from '@ngrx/store';
import { AppUser } from 'src/app/models/appState/app-user.model';

export const userAuthenticated = createAction(
  '[Auth] User Authenticated',
  props<AppUser>()
);

export const enterpriseIdChanged = createAction(
  '[Auth] Enterprise Changed',
  props<{ enterpriseId: string }>()
);
