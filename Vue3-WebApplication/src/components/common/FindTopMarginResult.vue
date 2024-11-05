<template>
    <div>
        <div>
            Sure, we've found the results of your last optimization, which were for model [{{intentRes.modelName}}]'s [{{intentRes.caseName}}] case. Below are the top [{{intentRes.topNumber}}] factors that have the most significant impact on economic values:
        </div>
        <div style="margin-top: 20px;">Increase the max value of these variables can increase OBJ</div>
            <FindTopGrid :tableData="increaseTableData"></FindTopGrid>
        <div style="margin-top: 20px;">Decrease the min value of these variables can increase OBJ</div>
            <FindTopGrid :tableData="decreaseTableData"></FindTopGrid>
        <div style="margin-top: 20px;">
            Please review these findings and let us know if you need further analysis.
        </div>
        <LineChart v-if="showChart" :chartData="chartData"></LineChart>
    </div>
</template>

<script lang="ts" setup >
import { onMounted, Ref, ref } from 'vue';
import LineChart from '../Charts/lineChart.vue';
import FindTopGrid from '../Grids/FindTopGrid.vue';
import {IChartData} from '../Charts/lineChart.vue'
import {getNonBasisType} from '../../utils/commonUtil'
const {data} = defineProps({data: Object})
const {intentRes, margins } = data;
const showChart = ref(false)

const increaseTableData = ref([]);
const decreaseTableData = ref([]);

const chartData: Ref<IChartData> = ref({
    xAxis:[],
    series: [{
        name: 'margin',
        type: 'bar',
        barWidth: 50,
        data: []
    }]
});

onMounted(() => {
    margins.forEach(margin => {
        let rowData = Object.assign({}, margin);
        rowData.type = getNonBasisType(margin.nonBasisType);
        rowData.margin = rowData.margin.toFixed(3)
        margin.margin > 0? increaseTableData.value.push(rowData) : decreaseTableData.value.push(rowData);
        chartData.value.xAxis.push(`${margin.variableName} \n (${rowData.type})`);
        chartData.value.series[0].data.push(rowData.margin);
    });
    showChart.value = true
})

</script>

<style scoped>
.variableName{
    margin-right: 10px;
}
</style>