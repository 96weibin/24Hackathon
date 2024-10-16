<template>
 <div style="width:70%;margin:0 auto;">
    <h1 style="text-align:center;">Ask a Question</h1>
    <p style="text-align:center;">
        Need help with a technical question or product issue? Asking a question is
        the best way to get help.
    </p>
    <p>Question</p>
    <el-input v-model="title" placeholder="Please input" />
    <p>Details</p>
    <el-input
        v-model="content"
        :autosize="{ minRows: 4, maxRows: 8 }"
        type="textarea"
        placeholder="Please input"
    />
    <p>Products:</p>
    <el-select style="margin-right:20px" disabled v-model="familyValue" class="m-2" placeholder="Select">
        <el-option
        v-for="item in familyOptions"
        :key="item.value"
        :label="item.label"
        :value="item.value"
        />
    </el-select>
    <el-select disabled v-model="typeValue" class="m-2" placeholder="Select">
        <el-option
        v-for="item in options"
        :key="item.value"
        :label="item.label"
        :value="item.value"
        />
    </el-select>
    <p>
        <el-checkbox v-model="checkedPrivate" label="Private" size="large" />
    </p>
    <p v-if="isAdd"><el-button @click="submit" type="primary">Submit</el-button></p>
    <p v-else>
        <el-button @click="cancel">Cancel</el-button>
        <el-button @click="confirm" type="primary">Confirm</el-button>
    </p>
  </div>
</template>
<script lang="ts">
import AskedQuestion from "./asked-question";
export default AskedQuestion;
import { ref } from "vue";
</script>
<style scoped>
.info {
  display: flex;
  justify-content: space-between;
  flex-direction: row;
  align-items: center;
  color: #0078d4;
  height: 45px;
}
.user-icon {
  width: 45px;
  height: 45px;
  line-height: 45px;
  text-align: center;
  font-weight: bold;
  color: #e5e5e5;
  background: #0078d4;
  border-radius: 50%;
  font-size: 18px;
}
.user-name {
  margin-left: 10px;
}
</style>
