import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectRoundGroupComponent } from './select-round-group.component';

describe('SelectRoundGroupComponent', () => {
  let component: SelectRoundGroupComponent;
  let fixture: ComponentFixture<SelectRoundGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelectRoundGroupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectRoundGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
