import { TestBed, inject } from '@angular/core/testing';

import { PropertyDescriptorConverterService } from './property-descriptor-converter.service';

describe('PropertyDescriptorConverterService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PropertyDescriptorConverterService]
    });
  });

  it('should ...', inject([PropertyDescriptorConverterService], (service: PropertyDescriptorConverterService) => {
    expect(service).toBeTruthy();
  }));
});
