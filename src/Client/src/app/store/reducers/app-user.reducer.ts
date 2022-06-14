import { createReducer, on } from '@ngrx/store';
import { AppUser } from 'src/app/models/appState/app-user.model';
import {
  enterpriseIdChanged,
  userAuthenticated
} from '../actions/appState.actions';

const appUserInitialState: AppUser = {
  userId: -1,
  name: '',
  email: ''
};

export const appUserReducer = createReducer(
  appUserInitialState,
  on(userAuthenticated, (_, user) => user),
  on(enterpriseIdChanged, (state, action: { enterpriseId: string }) => {
    let a = state.enterpriseIds?.indexOf(action.enterpriseId);
    if (
      state.enterpriseIds &&
      state.enterpriseIds.indexOf(action.enterpriseId) >= 0
    ) {
      const newState = Object.assign({}, state);
      newState.enterpriseId = action.enterpriseId;
      return newState;
    }
    return state;
  })
);
