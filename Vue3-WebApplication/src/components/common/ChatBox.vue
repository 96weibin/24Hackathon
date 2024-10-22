<template>  
  <div class="chat-container">  
    <div class="messages">  
      <div  
        v-for="(message, index) in messages"  
        :key="index"  
        :class="['message', message.isSender ? 'sender' : 'receiver']" 
      >  
        <span>{{ message.content }}</span>  
        {{ message.isChart }}
        <LineChart :data="message.data" :msg="message.content" v-if="message.isChart"></LineChart>
        <div v-for="(item, index) in message.intents" :key="index">
          <el-button type="info" @click="sendMessage(item.category)">{{item.category}}</el-button>
        </div>
      </div>  
    </div>  
    <div class="input-container">  
      <el-input  
        v-model="newMessage"  
        autofocus
        placeholder="analysis input..."  
        class="input-box"  
        @keyup.enter="sendMessage"  
      />  
      <el-button type="primary" @click="sendMessage">Send</el-button>  
    </div>  
  </div>  
</template>  
  
<script lang="ts" setup>  
import { ref, onMounted } from 'vue';  
import axios from 'axios';
import LineChart from '../Charts/lineChart.vue';
interface Message {  
  content: string;  
  isSender: boolean; // true 表示发送者（右边），false 表示接收者（左边）  
  isChart?: boolean;
  intents?: any[];
  data?: any;

}  

onMounted(()=> {})



const messages = ref<Message[]>([  
  {content: "hello, I am AI assistant for AUP Economic.  What can I do for you?", isSender: false}
]);  

const newMessage = ref<string>('');  

const postMsg = (text) => {
  const url = "https://langcreatedinus.cognitiveservices.azure.com/language/:analyze-conversations?api-version=2022-10-01-preview";
  const config = {
    "headers": {
      "Ocp-Apim-Subscription-Key": "c1c2002fbc0f4af69af1b852d6945293",
      "Content-Type": "application/json"
    }
  }
  const data = {
    "kind": "Conversation",
    "analysisInput": {
      "conversationItem": {
        "id": "1",
        "participantId": "1",
        "text": text
      }
    },
    "parameters": {
      "projectName": "projcreatedinus",
      "deploymentName": "langdeloy",
      "stringIndexType": "TextElement_V8"
    }
  }
  return axios.post(url, data, config)
}
// 发送消息的方法  
const sendMessage = (message?: string) => {  
  let sendStr = newMessage.value.trim().length == 0? message: newMessage.value.trim();
  if (sendStr) {  
    messages.value.push({ content: sendStr, isSender: true });  
    postMsg(sendStr).then((res: any) => {
      console.log(res)
      let newRes: Message = { content: res.data.result.prediction.topIntent, isSender: false, intents: res.data.result.prediction.intents, data: res.data};

      if(Math.random()> 0.5) {
        console.log('has Chart......')
        newRes.isChart = true;
      }
      messages.value.push(newRes);  
    })

    // 清空输入框  
    newMessage.value = '';  
  }  
};  
  
</script>  
  
<style scoped>  
.chat-container {  
  display: flex;  
  flex-direction: column;  
  height: 100%; /* 使聊天室充满整个视口高度 */  
  width: 100%;  
  max-width: 500px; /* 可根据需要调整宽度 */  
  margin: 0 auto;  
  box-sizing: border-box;  
  padding: 10px;  
  background-color: #f5f5f5;  
}  
  
.messages {  
  flex: 1; /* 使消息区域占据剩余空间 */  
  overflow-y: auto; /* 允许垂直滚动 */  
  padding-right: 10px; /* 为右对齐的消息留出空间 */  
  display: flex;  
  flex-direction: column; /* 修改为 column 以正常顺序显示消息 */  
}  
  
.message {  
  max-width: 80%; /* 限制消息的最大宽度 */  
  padding: 10px;  
  border-radius: 10px;  
  margin-bottom: 10px;  
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);  
}  
  
.sender {  
  align-self: flex-end; /* 右对齐 */  
  background-color: #dcf8c6; /* 发送者消息背景色 */  
  color: #004000; /* 发送者消息文字颜色 */  
}  
  
.receiver {  
  align-self: flex-start; /* 左对齐 */  
  background-color: #ffe4e1; /* 接收者消息背景色 */  
  color: #8b0000; /* 接收者消息文字颜色 */  
}  
  
.input-container {  
  display: flex;  
  justify-content: space-between;  
  align-items: center;
  padding: 10px;  
  background-color: #fff;  
  border-top: 1px solid #ebeef5;  
  box-shadow: 0 -2px 4px rgba(0, 0, 0, 0.1);  
}  
  
.input-box {  
  flex: 1;  
  padding: 5px;  
  border: 1px solid #dcdcdc;  
  border-radius: 5px;  
}  
  
/* 可选：为输入框和按钮添加一些额外的样式 */  
.el-input__inner {  
  border: none;  
  outline: none;  
}  
  
.el-button {  
  padding: 5px 15px;  
}  
</style>