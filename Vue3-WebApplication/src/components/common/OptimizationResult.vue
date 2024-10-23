<template>
    <div>
        <div>
            Sure, we've found the results of your last optimization, which were for model [{{data.modelName}}]'s [{{data.caseName}}] case. Below are the top [{{data.topNumber}}] factors that have the most significant impact on economic benefits:
        </div>
            <ul>
                <li v-for="(item, i) in chartData.data" key="item">

                    [{{chartData.xAxis[i]}}] - The [{{chartData.text}}  ] of [{{chartData.xAxis[i]}}]  , contributing to [{{ item }}] % of impact.
                </li>
            </ul>
            <div>
                Please review these findings and let us know if you need further analysis
            </div>
    </div>
</template>

<script lang="ts" setup >
import { onMounted } from 'vue';
import {ChatResult, ChatService} from '../../services/chatService'

const {data} = defineProps({data: Object})
const chartData = data.chartData
onMounted(() => {

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