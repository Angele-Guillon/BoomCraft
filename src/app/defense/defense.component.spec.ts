import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DefenseComponent } from './defense.component';

describe('DefenseComponent', () => {
  let component: DefenseComponent;
  let fixture: ComponentFixture<DefenseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DefenseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
