import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskMembersComponent } from './task-members.component';

describe('TaskMembersComponent', () => {
  let component: TaskMembersComponent;
  let fixture: ComponentFixture<TaskMembersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskMembersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskMembersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
