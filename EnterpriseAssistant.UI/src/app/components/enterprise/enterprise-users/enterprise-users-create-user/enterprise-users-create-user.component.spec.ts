import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnterpriseUsersCreateUserComponent } from './enterprise-users-create-user.component';

describe('EnterpriseUsersCreateUserComponent', () => {
  let component: EnterpriseUsersCreateUserComponent;
  let fixture: ComponentFixture<EnterpriseUsersCreateUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnterpriseUsersCreateUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnterpriseUsersCreateUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
