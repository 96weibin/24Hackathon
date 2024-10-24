<template>
    <div>
        <div>
            Sure, we've found the results of your last optimization, which were for model [{{data.modelName}}]'s [{{data.caseName}}] case. Below are the top [{{data.topNumber}}] factors that have the most significant impact on economic benefits:
        </div>
            <ul>
                <li v-for="(item, i) in chartData.data" key="item">
                    {{chartData.xAxis[i].slice(0, chartData.xAxis[i].indexOf("\n"))}} - The {{chartData.xAxis[i].slice(chartData.xAxis[i].indexOf("(") + 1, chartData.xAxis[i].indexOf(")"))}} of {{chartData.xAxis[i].slice(0, chartData.xAxis[i].indexOf("\n"))}}. {{ item.toFixed(2)>0 ? "Increase the max value of these variables can increase OBJ " : "Decrease the min value of these variables can increase OBJ"}}.                
                </li>
            </ul>
            <div>
                Please review these findings and let us know if you need further analysis.
            </div>

            <LineChart :data="data"></LineChart>
    </div>
</template>

<script lang="ts" setup >
import { onMounted } from 'vue';
import {ChatResult, ChatService} from '../../services/chatService'
import LineChart from '../Charts/lineChart.vue';

const {data} = defineProps({data: Object})
const chartData = data.chartData
onMounted(() => {

    console.log("OptimizationResultP:" )
    console.log(chartData,data);
    
    
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