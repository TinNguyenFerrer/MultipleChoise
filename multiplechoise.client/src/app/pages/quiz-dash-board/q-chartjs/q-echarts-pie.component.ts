import { Component, Input, OnChanges, OnDestroy } from '@angular/core';
import { NbThemeService } from '@nebular/theme';

@Component({
  selector: 'ngx-q-echarts-pie',
  template: `
    <div echarts [options]="options" class="echart"></div>
  `,
})
export class QEchartsPieComponent implements OnChanges, OnDestroy {
  @Input() data: { value: number, name: string }[] = [];

  options: any = {};
  themeSubscription: any;

  constructor(private theme: NbThemeService) {
  }

  ngOnChanges() {
    this.themeSubscription = this.theme.getJsTheme().subscribe(config => {

      const colors = config.variables;
      const echarts: any = config.variables.echarts;

      this.options = {
        backgroundColor: echarts.bg,
        color: [colors.warningLight, colors.infoLight, colors.dangerLight, colors.successLight, colors.primaryLight],
        tooltip: {
          trigger: 'item',
          formatter: '{a} {b} : <br/> {c} ({d}%)',
        },
        legend: {
          orient: 'vertical',
          left: 'left',
          data: this.data.filter(d => d.value).map(i => i.name),
          textStyle: {
            color: echarts.textColor,
          },
        },
        series: [
          {
            name: 'Score',
            type: 'pie',
            radius: '80%',
            center: ['50%', '50%'],
            data: this.data.filter(d => d.value),
            itemStyle: {
              emphasis: {
                shadowBlur: 10,
                shadowOffsetX: 0,
                shadowColor: echarts.itemHoverShadowColor,
              },
            },
            label: {
              normal: {
                textStyle: {
                  color: echarts.textColor,
                },
              },
            },
            labelLine: {
              normal: {
                lineStyle: {
                  color: echarts.axisLineColor,
                },
              },
            },
          },
        ],
      };
    });
  }

  ngOnDestroy(): void {
    this.themeSubscription.unsubscribe();
  }
}
