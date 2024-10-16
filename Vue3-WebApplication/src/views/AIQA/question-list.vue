<template >
    <div class="question-list">'
     <div class="content">
            <el-link style="font-weight: bold;font-size: 20px;display:inline-block;color:#337ecc" @click="goToQuestionDetail">{{questionDetail.title}}</el-link>
            <span style="height:20px;line-height:20px;display:block;">{{questionDetail.name}}|{{questionDetail.time}}</span>
            <span style="height:40px;display:block;overflow:hidden;line-height:20px;">
                {{questionDetail.content}}
            </span>
        </div>
        <div class="row">
            <p class="cell"><span class="bold">{{questionDetail.replyCommentCount}}</span><span>replies</span></p>
            <p class="cell"><span class="bold">{{questionDetail.likeNumber}}</span><span>helpful</span></p>
            <p class="cell"><span class="bold">{{questionDetail.state}}</span><span>state</span></p>
        </div>
    </div>
  
</template>
<script lang="ts" setup>
debugger;
import { Vue } from "vue-class-component";
import basicInfo from './basic-info.vue';
import { useRouter } from 'vue-router';
import { QuestionService} from '../../services/QuestionService';
import { ref, toRefs,defineProps,defineEmits } from 'vue';

const props = defineProps({
    questionDetail:{
        title:String,
        name:String,
        content:String,
        time:String,
        replyCommentCount:Number,
        likeNumber:Number,
        state:String,
        id:Number
    }
});
const router = useRouter();
const goToQuestionDetail =()=>{
    debugger;
    router.push('/ai/' + props.questionDetail.id);
}
// const { name,count,time,content} = toRefs(props);
// console.log(props.name,props.count);
// debugger;
//   let showEdit = ref(false);     

// const confirm= ()=>{
//     confirm.value=false;
//      emit("updateComment",props.id);
//     debugger;
// }
// const cancel= ()=>{
//     showEdit.value=false;
// }
// const emit = defineEmits(['updateComment']);
// const edit=()=>{
//     debugger;
//      showEdit.value=true;
// }
    
</script>
<style scoped>
.content{
   flex:1
}
.row {
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: center;
    width:300px;
    margin-left:40px;
}
.cell{
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    flex:1;
}
.bold{
        font-weight:bold;
    }
.question-list{
    display: flex;
    flex-direction: row;
    align-items: center;
}
</style>