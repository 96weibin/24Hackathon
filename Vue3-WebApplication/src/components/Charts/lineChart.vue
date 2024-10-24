<template>
    <div>
        <div class="chartContainer" ref="myCharts"></div>
        <!-- {{ msg }} -->
    </div>
</template>

<script setup lang="ts">
import * as echarts from 'echarts'
import { onMounted, ref } from 'vue';
const myCharts = ref<HTMLDivElement | null>(null); 
const {msg, data} = defineProps({msg: String, data: Object})

const absChartData = data.chartData.data;
onMounted(()=>{
    initChart()
})

const initChart = ()=>{
    const chart = echarts.init(myCharts.value);
    let chartData = data.chartData;
    console.log('xAx............')
    console.log(data)
    chart.setOption({
        title: {
            text: chartData.text
        },
        tooltip: {},
        xAxis: {
            data: chartData.xAxis
        },
        yAxis: {},
        series: [
              {
                // name: ,
                type: 'bar',
                data: absChartData,
                barWidth: 50,
                // itemStyle: {  
                //     color: function (params) { 
                //         console.log(params)
                //         chartData.data[params.dataIndex]
                //         if (chartData.data[params.dataIndex] > 0) { // 注意：这里的params.value是基于传入的数据（可能是处理后的数据）  
                //             return 'red';  
                //         } else {  
                //             return 'green';  
                //         }  
                //     }  
                // }, 
              }
            ]
        });
  }
</script>

<style scoped>
.chartContainer{
        width: 1000px;
        height: 300px;
    }
</style>