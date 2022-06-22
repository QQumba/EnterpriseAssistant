import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskTrackModalComponent } from './task-track-modal.component';

describe('TaskTrackModalComponent', () => {
  let component: TaskTrackModalComponent;
  let fixture: ComponentFixture<TaskTrackModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskTrackModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskTrackModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
