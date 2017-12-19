import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NavRessourceComponent } from './nav-ressource.component';

describe('NavRessourceComponent', () => {
  let component: NavRessourceComponent;
  let fixture: ComponentFixture<NavRessourceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NavRessourceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavRessourceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
