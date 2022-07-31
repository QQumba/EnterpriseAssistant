import { Injectable } from '@angular/core';
import {
  Router,
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { TaskFacade } from '../store/task.facade';

@Injectable({
  providedIn: 'root'
})
export class TaskListResolver implements Resolve<boolean> {
  constructor(private facade: TaskFacade) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    this.facade.loadTasks();
    return of(true);
  }
}
