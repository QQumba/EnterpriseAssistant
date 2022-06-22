import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { catchError, map, of, switchMap } from 'rxjs';
import { DepartmentService } from 'src/app/services/department.service';
import { TaskService } from 'src/app/services/task.service';
import {
  addUserToDepartment,
  assignUserToTask,
  assignUserToTaskSuccess,
  createTask,
  createTaskSuccess,
  loadTasksByProjectId,
  loadTasksFailure,
  loadTasksSuccess,
  loadUserTasks
} from '../actions/task.actions';

@Injectable()
export class TaskEffects {
  constructor(
    private actions$: Actions,
    private taskService: TaskService,
    private store: Store,
    private departmentService: DepartmentService
  ) {}

  loadTasksByProjectId$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(loadTasksByProjectId),
      switchMap(({ projectId }) =>
        this.taskService.getTasksByProjectId(projectId).pipe(
          map((tasks) => loadTasksSuccess({ tasks })),
          catchError((error) => of(loadTasksFailure({ error })))
        )
      )
    );
  });

  loadUserTasks$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(loadUserTasks),
      switchMap(() =>
        this.taskService.getUserTasks().pipe(
          map((tasks) => loadTasksSuccess({ tasks })),
          catchError((error) => of(loadTasksFailure({ error })))
        )
      )
    );
  });

  assignUserToTask$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(assignUserToTask),
      switchMap(({ taskId, user }) =>
        this.taskService.assignUserToTasks(taskId, user).pipe(
          map((user) => assignUserToTaskSuccess({ taskId, user })),
          catchError((error) => of(loadTasksFailure({ error })))
        )
      )
    );
  });

  createTask$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(createTask),
      switchMap(({ task }) =>
        this.taskService.createTask(task).pipe(
          map((newTask) => createTaskSuccess({ task: newTask })),
          catchError((error) => of(loadTasksFailure({ error })))
        )
      )
    );
  });

  // addUserToDepartment$ = createEffect(() => {
  //   return this.actions$.pipe(
  //     ofType(addUserToDepartment),
  //     switchMap(({ departmentId, user }) =>
  //       this.departmentService.addUserToDepartment(departmentId, user).pipe(
  //         map((user) => assignUserToTaskSuccess({ departmentId, user })),
  //         catchError((error) => of(loadTasksFailure({ error })))
  //       )
  //     )
  //   );
  // });
}
