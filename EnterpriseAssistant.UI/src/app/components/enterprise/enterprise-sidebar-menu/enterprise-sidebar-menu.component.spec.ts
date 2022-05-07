import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnterpriseSidebarMenuComponent } from './enterprise-sidebar-menu.component';

describe('EnterpriseSidebarMenuComponent', () => {
  let component: EnterpriseSidebarMenuComponent;
  let fixture: ComponentFixture<EnterpriseSidebarMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnterpriseSidebarMenuComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnterpriseSidebarMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
