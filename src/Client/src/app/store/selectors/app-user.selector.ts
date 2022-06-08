import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AppUser } from 'src/app/models/appState/app-user.model';

export const selectAppUser = createFeatureSelector<AppUser>('appUser');

export const selectEnterpriseId = createSelector(selectAppUser, (appUser) => {
  return appUser.enterpriseId;
});
