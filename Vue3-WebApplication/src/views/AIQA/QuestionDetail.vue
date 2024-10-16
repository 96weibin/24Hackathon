<template>
  <!--<h1>QuestionDetail</h1>
  -->
  <div class="question-detail">
    <div class="question-detail-body">
      <basic-info :name="name" :time="createTime"></basic-info>
      <h1 class="title">{{questionTitle}}</h1>
      <div class="content">
        {{content}}
      </div>
      <p style="margin-bottom:20px;">
        <el-button type="primary" @click="reply">Reply</el-button>
        <el-button @click="edit">Edit</el-button>
      </p>
      <div v-show="showEdit" style="margin-bottom:20px;margin-top:20px">
        <el-input v-model="myComment" :autosize="{ minRows: 2, maxRows: 4 }"
      type="textarea" placeholder="Please input" style="margin-bottom:20px;" />
        <el-button @click="cancel">Cancel</el-button>
        <el-button type="primary" @click="submit">Submit</el-button>
      </div>
       <el-collapse class="margin-top:40px">
         <el-collapse-item :title="'Replies '+replyCount" >
         <div v-for="(com,index) in replyComments" style="margin-bottom:20px;background:#f7f7f7;padding:10px;">
         <reply-card :isReply="true"  :key="index" v-if="replyCount!=0" :name="com.personName" 
            :time="com.createDate" :count="com.likeNumber" :content="com.content" 
            :id="com.id" :isLike="com.isLikeComment" :disLike="com.isDislikeComment"></reply-card>
         </div>
         </el-collapse-item>
       </el-collapse>
       <div style="margin-top:20px" v-if="hideSummary">
        <el-button :disabled="disableSummaryBtn" @click="closeNoSummary">Close</el-button>
        <el-button @click="closeWithSummary" :disabled="disableSummaryBtn">Close with summary</el-button>
        <div v-if="showSummary" style="margin-top:20px;background:#f7f7f7">
          <el-input style="margin-bottom:10px" v-model="mySummary"  :autosize="{ minRows: 2, maxRows: 4 }"
              type="textarea" placeholder="Please input"/>
          <el-button @click="cancelSummary">Cancel</el-button>
          <el-button @click="submitSummary">Submit</el-button>
        </div>
       </div>
      
     
    </div>
    <div class="question-info">
      <span class="info-title">Question Info</span>
      <el-divider />
      <p><span>Applies to:</span><span>{{familyName}}\{{questionType}}</span></p>
       <el-checkbox disabled>Link VSTS</el-checkbox>
       <el-checkbox disabled>Auto Async VSTS</el-checkbox>
       <div v-if="showRefer">
        <p>Invite RND developers to answer:</p>
        <el-select style="margin-bottom:20px" v-model="value" placeholder="@ somebody to answer question"  multiple filterable @change="selectPeople">
          <el-option
            v-for="item in rndPeople"
            :key="item.value"
            :label="item.label"
            :value="item">
          </el-option>
        </el-select>

         <p>Add your comments:</p>
        <div style="margin-top:10px;margin-bottom:10px;">
            <el-input v-model="referComment" :autosize="{ minRows: 2, maxRows: 4 }"
        type="textarea" placeholder="Please input" style="margin-bottom:20px;"/>
          <el-button @click="referCommentCancel">Cancel</el-button>
          <el-button type="primary" @click="referCommentSubmit">Submit</el-button>
        </div>  
        <el-collapse class="margin-top:40px">
          <el-collapse-item :title="'Refer '+referCount" >  
              <div v-for="(com,index) in referComments" style="margin-bottom:20px;background:#f7f7f7;padding:10px;">
                <reply-card :isReply="false" :key="index" v-if="referComments.length!=0" :name="com.personName" :time="com.createDate" :count="com.likeNumber" :content="com.content"
                :id="com.id"></reply-card>
            </div>
          </el-collapse-item>
        </el-collapse>
       </div>
    </div>
  </div>        
</template>
s
<script lang="ts">
import QuestionDetail from './QuestionDetail';
export default QuestionDetail
</script>

<style lang="less" scoped>
.question-detail{
    display: flex;
    flex-direction: row;
    justify-content: center
}
.question-detail-body {
  width:calc(~'100%' - 380px);
  margin-right:100px;
  .title {
    font-size: 2em;
  }
}
.content{
  margin-bottom:20px;
}
.question-info{
  width:380px;
  .info-title{
    height: 45px;
    line-height: 45px;
    font-weight: bold;
    font-size: 20px;
  }
}
    :deep(.el-collapse-item__header){
      font-size: 20px;
      font-weight: bold;
    }
   :deep(.el-collapse-item__content){
    // background:#f7f7f7;
   }
</style>