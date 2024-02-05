import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FailedComponentComponent } from './failed-component.component';

describe('FailedComponentComponent', () => {
  let component: FailedComponentComponent;
  let fixture: ComponentFixture<FailedComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FailedComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FailedComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
