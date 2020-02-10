/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { YourmapComponent } from './yourmap.component';

describe('YourmapComponent', () => {
  let component: YourmapComponent;
  let fixture: ComponentFixture<YourmapComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YourmapComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YourmapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
