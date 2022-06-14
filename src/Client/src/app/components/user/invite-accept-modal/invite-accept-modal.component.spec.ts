import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InviteAcceptModalComponent } from './invite-accept-modal.component';

describe('InviteAcceptModalComponent', () => {
  let component: InviteAcceptModalComponent;
  let fixture: ComponentFixture<InviteAcceptModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InviteAcceptModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InviteAcceptModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
