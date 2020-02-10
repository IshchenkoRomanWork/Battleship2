/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UnaddedshipsService } from './unaddedships.service';

describe('Service: Unaddedships', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UnaddedshipsService]
    });
  });

  it('should ...', inject([UnaddedshipsService], (service: UnaddedshipsService) => {
    expect(service).toBeTruthy();
  }));
});
