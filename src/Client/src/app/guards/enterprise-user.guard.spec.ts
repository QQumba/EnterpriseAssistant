import { TestBed } from '@angular/core/testing';

import { EnterpriseUserGuard } from './enterprise-user.guard';

describe('EnterpriseUserGuard', () => {
  let guard: EnterpriseUserGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(EnterpriseUserGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
