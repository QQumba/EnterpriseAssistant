import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnterpriseSidebarEnterprisePickerComponent } from './enterprise-sidebar-enterprise-picker.component';

describe('EnterpriseSidebarEnterprisePickerComponent', () => {
  let component: EnterpriseSidebarEnterprisePickerComponent;
  let fixture: ComponentFixture<EnterpriseSidebarEnterprisePickerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnterpriseSidebarEnterprisePickerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnterpriseSidebarEnterprisePickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
