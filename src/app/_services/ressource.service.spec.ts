import { TestBed, inject } from '@angular/core/testing';

import { RessourceService } from './ressource.service';

describe('RessourceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RessourceService]
    });
  });

  it('should be created', inject([RessourceService], (service: RessourceService) => {
    expect(service).toBeTruthy();
  }));
});
