import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormSegmentComponent } from './form-segment.component';

describe('FormSegmentComponent', () => {
  let component: FormSegmentComponent;
  let fixture: ComponentFixture<FormSegmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormSegmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormSegmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
