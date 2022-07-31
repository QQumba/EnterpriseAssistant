import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskListComponent } from './pages/task/components/task-list/task-list.component';
import { TaskListResolver } from './pages/task/resolvers/task-list.resolver';

const routes: Routes = [
  {
    path: 'tasks',
    component: TaskListComponent,
    resolve: { tasks: TaskListResolver }
  },
  { path: '', redirectTo: 'tasks', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
