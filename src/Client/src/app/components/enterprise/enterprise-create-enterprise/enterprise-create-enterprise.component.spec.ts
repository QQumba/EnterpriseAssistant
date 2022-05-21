import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnterpriseCreateEnterpriseComponent } from './enterprise-create-enterprise.component';

describe('EnterpriseCreateEnterpriseComponent', () => {
  let component: EnterpriseCreateEnterpriseComponent;
  let fixture: ComponentFixture<EnterpriseCreateEnterpriseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnterpriseCreateEnterpriseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnterpriseCreateEnterpriseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
