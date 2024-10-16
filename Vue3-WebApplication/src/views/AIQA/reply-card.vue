<template style="margin-bottom:10px">

   <basic-info :name="name" :time="myTime"></basic-info>
     <p>{{content}}</p>
     <el-button @click="edit" type="primary">Edit</el-button>
    <p>
    <div v-if="isReply">
        <span>Was this reply helpful?</span>
        <el-button text type="primary" disabled="isLike" @click="likeComment">Yes</el-button>
        <el-button text type="primary" disabled="disLike" @click="dislikeComment">No</el-button>
    </div>
    </p>
    
     <div ref="textareaRef" v-show="showEdit">
        <el-input v-model="content" :autosize="{ minRows: 2, maxRows: 4 }"
        type="textarea" placeholder="Please input"/>
        <el-button @click="cancel">Cancel</el-button><el-button @click="confirm">Confirm</el-button>
    </div>
  
</template>
<script lang="ts" setup>
import { Vue } from "vue-class-component";
import basicInfo from './basic-info.vue';
import { useRoute } from 'vue-router';
import { QuestionService} from '../../services/QuestionService';
import { ref, toRefs,defineProps,defineEmits } from 'vue';
import { DateUtil } from "../../utils/dateUtil";

const props = defineProps({
  name: {
    type: String,
    // required: true,
    // default:'sdfsdf'
  },
  count:{
    type:Number,
    // required: true,
    // default:0
  },
  content:{
    type: String,
    // required: true,
    // default:'dfasdfasd'
  },
  time:{
    type:String
    // ,default:'3r3rr'
  },
  id:{
    type:Number
  },
  like:{
    type:Boolean
  },
  disLike:{
    type:Boolean
  },
  isReply:{
    type:Boolean
  }
});
// const { name,count,time,content} = toRefs(props);
const myTime = DateUtil.formatShortDate(props.time);
console.log(props.name,props.count);
debugger;
  let showEdit = ref(false);     
const questionService = new QuestionService();
const confirm= ()=>{
       let comment: IUpdateCommentRequest = {
            id: props.id,
            content: props.content,
        }
        questionService.updateComment(comment).then(res => {
            debugger;
        });
     showEdit.value=false;
    debugger;
}
const cancel= ()=>{
    showEdit.value=false;
}
const emit = defineEmits(['updateComment']);
const edit=()=>{
    debugger;
     showEdit.value=true;
}
const likeComment=()=>{
  const userId = 6;
  questionService.likeComment(props.id,userId);
}
const dislikeComment=()=>{
  const userId = 6;
  questionService.dislikeComment(props.id,userId);
}
</script>
<style scoped>

</style>