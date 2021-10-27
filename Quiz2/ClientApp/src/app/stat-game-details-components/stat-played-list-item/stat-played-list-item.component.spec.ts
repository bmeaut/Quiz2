import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { StatPlayedListItem } from './stat-played-list-item.component';


describe('StatPlayedListItem', () => {
  let component: StatPlayedListItem;
  let fixture: ComponentFixture<StatPlayedListItem>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatPlayedListItem ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatPlayedListItem);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
