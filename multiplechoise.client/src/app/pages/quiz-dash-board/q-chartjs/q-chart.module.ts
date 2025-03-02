import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QEchartsPieComponent } from './q-echarts-pie.component';
import { NbCardModule } from '@nebular/theme';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { ChartModule } from 'angular2-chartjs';
import { NgxEchartsModule } from 'ngx-echarts';
import { ThemeModule } from '../../../@theme/theme.module';
import { QChartjsComponent } from './q-chartjs.component';
import { QEchartsBarComponent } from './q-echartjs-bar.component';

const components = [
  QChartjsComponent,
  QEchartsPieComponent,
  QEchartsBarComponent,
];


@NgModule({
  declarations: [...components],
  imports: [
      CommonModule,
      ThemeModule,
      NgxEchartsModule,
      NgxChartsModule,
      ChartModule,
      NbCardModule,
    ],
    exports: [...components],
})
export class QChartModule { }
