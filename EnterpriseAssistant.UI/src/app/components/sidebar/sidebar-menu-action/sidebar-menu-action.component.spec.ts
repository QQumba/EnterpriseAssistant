import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarMenuActionComponent } from './sidebar-menu-action.component';

describe('SidebarMenuActionComponent', () => {
  let component: SidebarMenuActionComponent;
  let fixture: ComponentFixture<SidebarMenuActionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SidebarMenuActionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SidebarMenuActionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
