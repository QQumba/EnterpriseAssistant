import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { loadTasks } from './actions/task.actions';
import { selectTasks } from './selectors/task.selectors';

@Injectable({ providedIn: 'root' })
export class TaskFacade {
  tasks$ = this.store.select(selectTasks);

  constructor(private store: Store) {}

  loadTasks(): void {
    this.store.dispatch(loadTasks());
  }
}
