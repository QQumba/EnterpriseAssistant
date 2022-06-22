import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentMembersComponent } from './department-members.component';

describe('DepartmentMembersComponent', () => {
  let component: DepartmentMembersComponent;
  let fixture: ComponentFixture<DepartmentMembersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DepartmentMembersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentMembersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
