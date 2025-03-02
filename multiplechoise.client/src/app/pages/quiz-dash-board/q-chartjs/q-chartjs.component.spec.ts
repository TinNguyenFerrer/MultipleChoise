import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QChartjsComponent } from './q-chartjs.component';

describe('QChartjsComponent', () => {
  let component: QChartjsComponent;
  let fixture: ComponentFixture<QChartjsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QChartjsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QChartjsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
