import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopUpRoundInfoComponent } from './pop-up-round-info.component';

describe('PopUpRoundInfoComponent', () => {
  let component: PopUpRoundInfoComponent;
  let fixture: ComponentFixture<PopUpRoundInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PopUpRoundInfoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopUpRoundInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
