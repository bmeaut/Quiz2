import{async,ComponentFixture, TestBed} from '@angular/core/testing';
import { StatListItemComponent } from './stat-list-item.component';

describe('StatListComponent', () => {
  let component: StatListItemComponent;
  let fixture: ComponentFixture<StatListItemComponent>;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatListItemComponent ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

