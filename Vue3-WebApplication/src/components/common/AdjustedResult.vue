<template>
    <div>
        <div>
            The new OBJ value is {{adjustObj[1].toFixed(2)}}, which is a {{num}}% {{adjustObj[0] - adjustObj[1] < 0? "increase" : "decrease"}} from the previous result of {{adjustObj[0].toFixed(2)}}.
        </div>
            <LineChart :chartData="chartData"></LineChart>
            <div>
                Would you like me to create a new case for you?
            </div>
    </div>
</template>

<script lang="ts" setup >
import { onMounted, ref, Ref } from 'vue';
import LineChart, { IChartData } from '../Charts/lineChart.vue';

const {data} = defineProps({data: Object})
const {adjustObj} = data;
const chartData: Ref<IChartData> = ref({
    text: "OBJ",
    xAxis:['OBJ1', 'OBJ2'],
    series: [{
        name: 'OBJ',
        type: 'bar',
        barWidth: 50,
        data: adjustObj
    }]
});
let num = (Math.abs(adjustObj[1] - adjustObj[0]) / adjustObj[0] * 100).toFixed(2)
onMounted(() => { })

</script>

<style scoped>

</style>