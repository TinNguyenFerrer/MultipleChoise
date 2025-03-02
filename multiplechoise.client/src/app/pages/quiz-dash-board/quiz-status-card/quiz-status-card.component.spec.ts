import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizStatusCardComponent } from './quiz-status-card.component';

describe('QuizStatusCardComponent', () => {
  let component: QuizStatusCardComponent;
  let fixture: ComponentFixture<QuizStatusCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuizStatusCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuizStatusCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
