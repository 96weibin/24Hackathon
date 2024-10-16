<template>
  <div class="qa-view">
    <div class="filter">
       <h1 style="margin-right: 40px;">Q&A</h1>
       <span style="margin-right:20px;">Products:</span>
       <el-select v-model="typeValue" class="m-2" placeholder="Select" @change="filterQuestionByProduct">
          <el-option
              v-for="item in options"
              :key="item.value"
              :label="item.label"
              :value="item.value"
          />
       </el-select>
    </div>
       <el-divider />
       <p style="text-align:right">
        <el-radio-group v-model="defaultCheck" @change="changeBtn">
          <el-radio label="0">All</el-radio>
          <el-radio label="1">Open</el-radio>
          <el-radio label="2">Close</el-radio>
        </el-radio-group>
       <span style="margin-left:20px">Sort by </span>
       <el-button link type='primary' text="primary">Latest
       <el-icon v-show="showDown" @click="filterQuestion"><Bottom /></el-icon>
       <el-icon v-show="!showDown" @click="filterQuestion"><Top /></el-icon>
       </el-button></p>
       <question-list style="margin-bottom:10px; background: #f2f2f2;padding:20px" 
        v-for="(question,index) in questionList"
        :questionDetail="question" :key="index" ></question-list>
     
  </div>
    <el-pagination background layout="prev, pager, next" :total="totalCount" :page-size=size
     @current-change="handleCurrentChange" />
</template>

<script>
import QAList from "./QAList";
export default QAList
</script>

<style lang="less" scoped>
  .qa-view {
    // background: #f2f2f2;
  }
  .filter{
    display: flex;
    flex-direction: row;
    align-items: center;
  }
</style>