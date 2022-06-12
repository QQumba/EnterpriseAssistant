import { TestBed } from '@angular/core/testing';

import { EnterpriseUserValidatorService } from './enterprise-user-validator.service';

describe('EnterpriseUserValidatorService', () => {
  let service: EnterpriseUserValidatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EnterpriseUserValidatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
