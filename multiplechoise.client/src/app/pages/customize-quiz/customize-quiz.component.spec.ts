import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomizeQuizComponent } from './customize-quiz.component';

describe('CustomizeQuizComponent', () => {
  let component: CustomizeQuizComponent;
  let fixture: ComponentFixture<CustomizeQuizComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomizeQuizComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomizeQuizComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
