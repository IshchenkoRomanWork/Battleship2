/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { OpponentsmapComponent } from './opponentsmap.component';

describe('OpponentsmapComponent', () => {
  let component: OpponentsmapComponent;
  let fixture: ComponentFixture<OpponentsmapComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpponentsmapComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpponentsmapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
