import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SendInputControlComponent } from './send-input-control.component';

describe('SendInputControlComponent', () => {
  let component: SendInputControlComponent;
  let fixture: ComponentFixture<SendInputControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SendInputControlComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SendInputControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
