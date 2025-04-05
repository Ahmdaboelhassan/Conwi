import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MessagesHeaderComponent } from './messages-header.component';

describe('MessagesHeaderComponent', () => {
  let component: MessagesHeaderComponent;
  let fixture: ComponentFixture<MessagesHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MessagesHeaderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MessagesHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
