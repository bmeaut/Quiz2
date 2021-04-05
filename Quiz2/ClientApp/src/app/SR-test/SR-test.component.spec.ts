import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SRTestComponent } from './SR-test.component';

describe('QuizQuestionComponent', () => {
  let component: SRTestComponent;
  let fixture: ComponentFixture<SRTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SRTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SRTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
