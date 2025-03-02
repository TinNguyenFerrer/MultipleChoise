import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizDashBoardComponent } from './quiz-dash-board.component';

describe('QuizDashBoardComponent', () => {
  let component: QuizDashBoardComponent;
  let fixture: ComponentFixture<QuizDashBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuizDashBoardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuizDashBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
