import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskAssignModalComponent } from './task-assign-modal.component';

describe('TaskAssignModalComponent', () => {
  let component: TaskAssignModalComponent;
  let fixture: ComponentFixture<TaskAssignModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskAssignModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskAssignModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
