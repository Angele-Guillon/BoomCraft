import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArmeeComponent } from './armee.component';

describe('ArmeeComponent', () => {
  let component: ArmeeComponent;
  let fixture: ComponentFixture<ArmeeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArmeeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArmeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
