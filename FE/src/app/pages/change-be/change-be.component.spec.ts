import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeBEComponent } from './change-be.component';

describe('ChangeBEComponent', () => {
  let component: ChangeBEComponent;
  let fixture: ComponentFixture<ChangeBEComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChangeBEComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChangeBEComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
