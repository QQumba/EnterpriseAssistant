import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, of, switchMap } from 'rxjs';
import { TaskService } from '../../services/task.service';
import {
  loadTasks,
  loadTasksError,
  loadTasksSuccess
} from '../actions/task.actions';

@Injectable()
export class TaskEffects {
  loadTasks$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(loadTasks),
      switchMap(() =>
        this.serivce.getTasksByProjectId(0).pipe(
          map((tasks) => loadTasksSuccess({ tasks })),
          catchError((error) => of(loadTasksError({ error })))
        )
      )
    );
  });

  constructor(private actions$: Actions, private serivce: TaskService) {}
}
