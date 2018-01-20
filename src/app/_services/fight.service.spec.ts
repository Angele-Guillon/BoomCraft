import { TestBed, inject } from '@angular/core/testing';

import { FightService } from './fight.service';

describe('FightService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FightService]
    });
  });

  it('should be created', inject([FightService], (service: FightService) => {
    expect(service).toBeTruthy();
  }));
});
