import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnterpriseInvitesComponent } from './enterprise-invites.component';

describe('EnterpriseInvitesComponent', () => {
  let component: EnterpriseInvitesComponent;
  let fixture: ComponentFixture<EnterpriseInvitesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnterpriseInvitesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnterpriseInvitesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
