<template>
    <div>
        <div class="chartContainer" ref="myCharts"></div>
        {{ msg }}
    </div>
</template>

<script setup lang="ts">
import * as echarts from 'echarts'
import { onMounted, ref } from 'vue';
const myCharts = ref<HTMLDivElement | null>(null); 
const {msg, data} = defineProps({msg: String, data: Object})

onMounted(()=>{
    initChart()
})

const initChart = ()=>{
    const chart = echarts.init(myCharts.value);
    chart.setOption({
        title: {
            // text: 'ECharts 入门示例'
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
</script>

<style scoped>
.chartContainer{
        width: 400px;
        height: 200px;
    }
</style>