<template>
    <div>
        <div>
            The new OBJ value is {{chartData.data[1].toFixed(2)}}, which is a {{num}}% {{isIncrease? "increase" : "decrease"}} from the previous result of {{chartData.data[0].toFixed(2)}}.</div>
            <LineChart :data="data"></LineChart>
            
            <div>
                Would you like me to create a new case for you?
            </div>
    </div>
</template>

<script lang="ts" setup >
import { onMounted } from 'vue';
import {ChatResult, ChatService} from '../../services/chatService'
import LineChart from '../Charts/lineChart.vue';

const {data} = defineProps({data: Object})
const chartData = data.chartData
let isIncrease = (chartData.data[1] - chartData.data[0]) > 0;
let num = (Math.abs(chartData.data[1] - chartData.data[0]) / chartData.data[0] * 100).toFixed(2)
onMounted(() => {
    console.log('adj data ......................')
    console.log(data)
})

// data.value = {
//     caseName :"Base",
//     category:1,
//     confidenceScore: 0.9066047,
//     modelName: "Gulf Coast",
//     nonBasisType:  0,
//     question: "Can you show me the top 5 basis which impact the economy fo Gulf Coast model Base case",
//     topNumber: 5
// }

</script>

<style scoped>

</style>