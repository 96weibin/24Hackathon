<template>
  <div>
    Charts
    <div class="chartContainer" ref="myCharts"></div>
    <div class="chartContainer" ref="funnel"></div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { Options, Vue } from 'vue-class-component';
import * as echarts from 'echarts'
import { reactive, ref } from 'vue';

export default class Charts extends Vue{
  mounted(): void {
    this.initChart();
    this.initFunnelChart();
  }
  private initFunnelChart() {
    const myCharts = this.$refs.funnel as HTMLElement
    const chart = echarts.init(myCharts);
    let options = {
      title: {
        text: 'Funnel',
        left: 'left',
        top: 'bottom'
      },
      tooltip: {
        trigger: 'item',
        formatter: '{a} <br/>{b} : {c}%'
      },
      toolbox: {
        orient: 'vertical',
        top: 'center',
        feature: {
          dataView: { readOnly: false },
          restore: {},
          saveAsImage: {}
        }
      },
      legend: {
        orient: 'vertical',
        left: 'left',
        data: ['Show', 'Click', 'Visit', 'Inquiry', 'Order']
      },

      series: [
        {
          name: 'Funnel',
          type: 'funnel',
          width: '40%',
          height: '45%',
          left: '5%',
          top: '50%',
          data: [
            { value: 60, name: 'Visit' },
            { value: 30, name: 'Inquiry' },
            { value: 10, name: 'Order' },
            { value: 80, name: 'Click' },
            { value: 100, name: 'Show' }
          ]
        },
        {
          name: 'Pyramid',
          type: 'funnel',
          width: '40%',
          height: '45%',
          left: '5%',
          top: '5%',
          sort: 'ascending',
          data: [
            { value: 60, name: 'Visit' },
            { value: 30, name: 'Inquiry' },
            { value: 10, name: 'Order' },
            { value: 80, name: 'Click' },
            { value: 100, name: 'Show' }
          ]
        },
        {
          name: 'Funnel',
          type: 'funnel',
          width: '40%',
          height: '45%',
          left: '55%',
          top: '5%',
          label: {
            position: 'left'
          },
          data: [
            { value: 60, name: 'Visit' },
            { value: 30, name: 'Inquiry' },
            { value: 10, name: 'Order' },
            { value: 80, name: 'Click' },
            { value: 100, name: 'Show' }
          ]
        },
        {
          name: 'Pyramid',
          type: 'funnel',
          width: '40%',
          height: '45%',
          left: '55%',
          top: '50%',
          sort: 'ascending',
          label: {
            position: 'left'
          },
          data: [
            { value: 60, name: 'Visit' },
            { value: 30, name: 'Inquiry' },
            { value: 10, name: 'Order' },
            { value: 80, name: 'Click' },
            { value: 100, name: 'Show' }
          ]
        }
      ]
    };

    chart.setOption(options)


  }
  private initChart() {
    const myCharts = this.$refs.myCharts as HTMLElement
    const chart = echarts.init(myCharts);
    chart.setOption({
        title: {
            text: 'ECharts 入门示例'
        },
        tooltip: {},
        xAxis: {
            data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']
        },
        yAxis: {},
        series: [
            {
            name: '销量',
            type: 'bar',
            data: [5, 20, 36, 10, 10, 20]
            }
        ]
        });
  }
}
</script>

<style scoped>
    .chartContainer{
        width: 800px;
        height: 400px;
    }
</style>
