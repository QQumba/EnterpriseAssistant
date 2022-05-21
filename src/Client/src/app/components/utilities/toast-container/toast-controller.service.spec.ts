import { TestBed } from '@angular/core/testing';

import { ToastControllerService as ToastControllerService } from './toast-controller.service';

describe('ToastServiceService', () => {
  let service: ToastControllerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ToastControllerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
