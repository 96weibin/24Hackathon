<template>
    <div>
        <div>
            Sure, we've found the results of your last optimization, which were for model [{{data.modelName}}]'s [{{data.caseName}}] case. Below are the top [{{data.topNumber}}] factors that have the most significant impact on economic benefits:
        </div>
            <ul>
                <li>
                    [Factor 1] - The [purchase  ] of [Factor 1]  , contributing to [percentage] % of impact.
                </li>
                <li>
                    [Factor 2] - The [Sale] of [Factor 2]  , contributing to [percentage] % of impact.
                </li>
                <li>
                    [Factor 3] - The [Sale] of [Factor 3]  , contributing to [percentage] % of impact.
                </li>
                <li>
                    [Factor 4] - The [Sale] of [Factor 4]  , contributing to [percentage] % of impact.
                </li>
                <li>
                    [Factor 5] - The [Sale] of [Factor 5]  , contributing to [percentage] % of impact.
                </li>
            </ul>
            <div>
                Please review these findings and let us know if you need further analysis
            </div>
    </div>
</template>

<script lang="ts" setup >
import {ChatResult, ChatService} from '../../services/chatService'

const {data} = defineProps({data: Object})

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