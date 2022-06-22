import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task-members',
  templateUrl: './task-members.component.html',
  styleUrls: ['./task-members.component.scss']
})
export class TaskMembersComponent implements OnInit {
  @Input() taskId!: number;

  members$?: Observable<User[]>;

  constructor(private service: TaskService) {}

  ngOnInit(): void {
    this.members$ = this.service.getTaskUsers(this.taskId);
  }
}
