import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import{StatOwnedDetailsComponent} from './stat-owned-details.component'

describe('StatOwnedDetailsComponent', () => {
  let component: StatOwnedDetailsComponent;
  let fixture: ComponentFixture<StatOwnedDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatOwnedDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatOwnedDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
