import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentSidebarMenuComponent } from './department-sidebar-menu.component';

describe('DepartmentSidebarComponent', () => {
  let component: DepartmentSidebarMenuComponent;
  let fixture: ComponentFixture<DepartmentSidebarMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DepartmentSidebarMenuComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentSidebarMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
