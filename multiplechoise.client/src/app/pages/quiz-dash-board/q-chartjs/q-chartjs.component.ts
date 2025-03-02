import { Component, Input } from '@angular/core';

@Component({
  selector: 'ngx-q-chartjs',
  templateUrl: './q-chartjs.component.html',
  styleUrls: ['./q-chartjs.component.scss']
})
export class QChartjsComponent {
  @Input() data: { value: number, name: string }[] = [];
}
