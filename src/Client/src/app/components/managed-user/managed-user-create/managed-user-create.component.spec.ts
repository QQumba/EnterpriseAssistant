import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagedUserCreateComponent } from './managed-user-create.component';

describe('ManagedUserCreateComponent', () => {
  let component: ManagedUserCreateComponent;
  let fixture: ComponentFixture<ManagedUserCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManagedUserCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagedUserCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
