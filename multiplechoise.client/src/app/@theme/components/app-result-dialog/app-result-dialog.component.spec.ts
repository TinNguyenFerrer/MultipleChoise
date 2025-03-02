import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppResultDialogComponent } from './app-result-dialog.component';

describe('AppResultDialogComponent', () => {
  let component: AppResultDialogComponent;
  let fixture: ComponentFixture<AppResultDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppResultDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AppResultDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
