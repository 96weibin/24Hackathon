<template>
    <div>
        <div class="chartContainer" ref="myCharts"></div>
    </div>
</template>

<script setup lang="ts">
import * as echarts from 'echarts'
import { onMounted, ref } from 'vue';
const myCharts = ref<HTMLDivElement | null>(null); 
const props = defineProps<{ chartData: IChartData }>()

onMounted(()=>{
    initChart();
})

const initChart = ()=>{
    const chart = echarts.init(myCharts.value);
    chart.setOption({
        title: { },
        tooltip: {},
        xAxis: {
            data: props.chartData.xAxis
        },
        yAxis: {},
        series: props.chartData.series
    });
}
  
export interface IChartData{
    xAxis: string[];
    series: any[];
}
</script>

<style scoped>
.chartContainer{
        width: 1000px;
        height: 300px;
    }
</style>