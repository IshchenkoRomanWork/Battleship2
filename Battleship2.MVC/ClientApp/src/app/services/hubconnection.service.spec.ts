/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { HubconnectionService } from './hubconnection.service';

describe('Service: Hubconnection', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HubconnectionService]
    });
  });

  it('should ...', inject([HubconnectionService], (service: HubconnectionService) => {
    expect(service).toBeTruthy();
  }));
});
