import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizStatusIconRendererComponent } from './quiz-status-icon-renderer-component.component';

describe('QuizStatusIconRendererComponentComponent', () => {
  let component: QuizStatusIconRendererComponent;
  let fixture: ComponentFixture<QuizStatusIconRendererComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuizStatusIconRendererComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuizStatusIconRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
