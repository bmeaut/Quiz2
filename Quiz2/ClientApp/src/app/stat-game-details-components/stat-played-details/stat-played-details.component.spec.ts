import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import{StatPlayedDetailsComponent} from './stat-played-details.component'

describe('StatPlayedDetailsComponent', () => {
  let component: StatPlayedDetailsComponent;
  let fixture: ComponentFixture<StatPlayedDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatPlayedDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatPlayedDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
