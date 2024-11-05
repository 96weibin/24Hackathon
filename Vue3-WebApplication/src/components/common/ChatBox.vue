<template>  
  <div class="chat-container">  
    <div class="messages">  
      <div  
        v-for="(message, index) in messages"  
        :key="index"  
        :class="['message', message.isSender ? 'sender' : 'receiver']" 
      >  
        <span>{{ message.text }}</span>  
        <FindTopMarginResult v-if="message.isFindTopMargin" :data="message.data"></FindTopMarginResult>
        <AdjustedResult v-if="message.isAdust" :data="message.data"></AdjustedResult>
        <div v-for="(item, index) in message.buttons" :key="index">
          <el-button style="margin-top: 5px;" type="success" @click="item.callback? item.callback(item.text): sendMessage(item.text)">{{item.text}}</el-button>
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
import { ref, onMounted, Ref } from 'vue';  
import FindTopMarginResult from '../common/FindTopMarginResult.vue'
import AdjustedResult from '../common/AdjustedResult.vue'
import { ChatService, Icategory, IFindTopResponse, IIntent } from '../../services/chatService'
interface Message {  
  text: string;  
  isSender: boolean; // true 表示发送者（右边），false 表示接收者（左边）  
  buttons?: {text: string, callback?: Function}[];
  isFindTopMargin?: boolean;
  isAdust?: boolean;
  data?:{
    intentRes?: IIntent;
    margins?: any[];
    adjustObj?: number[]
  }
}  
const intentList: Ref<IIntent[]> = ref([]);

const intentRes: Ref<IIntent> = ref({
  caseName : "Cat Cracker RTT vs FDR Study (Base)",
    category : 1,
    confidenceScore: 0.933620453,
    modelName: "Gulf Coast",
    nonBasisType: 0,
    question: "can you get the top 5 factors impact the economy of Base Model case of Gulf Coast model",
    topNumber: 5
})
const chatService = new ChatService();
onMounted(()=> { 
  initIntentList();
})

const initIntentList = () => {
  let listStr = localStorage.getItem("intentList");
  intentList.value = listStr? JSON.parse(listStr): [intentRes.value];
  localStorage.setItem('intentList', JSON.stringify(intentList.value))
}

// miss model name || miss case name
const findIntent = (intent: IIntent) => {
  let resIntent = intent;
  let intentListVal = intentList.value;
  let findIntent;
  if(!intent.modelName && !intent.caseName) {
    findIntent = intentListVal[intentListVal.length - 1];
    resIntent = findIntent || intent;
  } else if (intent.modelName) {
    findIntent = intentListVal.find(x => x.modelName == intent.modelName);
    resIntent = findIntent || intent;
  } else {
    findIntent = intentListVal.findLast(x => x.caseName == intent.caseName);
    resIntent = findIntent || intent;
  }
  resIntent.category = intent.category
  resIntent.topNumber = intent.topNumber || findIntent.topNumber
  resIntent.nonBasisType = intent.nonBasisType
  return resIntent;
}

// 同时有 caseName 和 model Name 的
const seaveIntent = (intent: IIntent) => {
  let findModelIntent = intentList.value.find((x) => x.modelName == intent.modelName);
  if(findModelIntent) {
    findModelIntent.caseName = intent.caseName;
    findModelIntent.topNumber = intent.topNumber;
  } else {
    intentList.value.push(intent)
  }
  localStorage.setItem('intentList', JSON.stringify(intentList.value))
}

const messages = ref<Message[]>([  
  {text: "Hello, I am AUP AI Assistant.  What can I do for you?", isSender: false}
]);  

const newMessage = ref<string>('');  

const accept = async (item)=> {
  messages.value.push({text: item, isSender: true})
  messages.value.push({text: "Ok, I will create a new case.", isSender: false});   
}

// 发送消息的方法  
const sendMessage = async (message?: string) => {  
  let sendStr = newMessage.value.trim().length == 0? message: newMessage.value.trim();
  if (sendStr) {  
    messages.value.push({ text: sendStr, isSender: true }); 
    anwserMessage(sendStr); 

  }  
};  

const checkIntent = (intent: IIntent) => {
  if(!intent.modelName || !intent.caseName || !intent.topNumber) {
    intent = findIntent(intent);
  } else {
    seaveIntent(intent)
  }
  return intent;
}

const anwserMessage = async (sendStr: string) => {
  debugger
  let intent = (await chatService.postIntent(sendStr)).data;
  intentRes.value = checkIntent(intent);

  console.log("ModelName:", intentRes.value.modelName, "CaseName: ", intentRes.value.caseName)
  let newRes: Message = { 
    text: "", 
    isSender: false, 
    data: {
      intentRes: intentRes.value,
    }
  };

  switch (intentRes.value.category) {
    case Icategory.None: 
      if(intentRes.value.question.indexOf('?') == -1) {
        newRes.text = 'You might want to try asking the following questions:'
      } else {
        newRes.text = "I'm sorry, I don't quite understand your question, but you might want to try asking the following questions:"
      }
      newRes.buttons = [
        {text: 'Can you show me the top 5 non basis factors which impact the economy? '},
        {text: 'Can you adjust to non-basis factors to achieve better economic benefits?'}
      ]
      break;
    case Icategory.FindTopMargin:
      let findTopRes: IFindTopResponse = (await chatService.postFindTopMargin(intentRes.value)).data;
      newRes.isFindTopMargin = true;
      newRes.data.margins = findTopRes.margins
      break;
    case Icategory.AdjustMargin:
      let adjRes = (await chatService.adjustMargin(intentRes.value)).data
      
      newRes.isAdust = true;
      newRes.data.adjustObj = [adjRes.obj1, adjRes.obj2];
      newRes.buttons = [
        {text: 'Yes', callback: accept},
        {text: 'No'}
      ]
      break;
    default:
      newRes.text = "success"
      break;
  }
  messages.value.push(newRes); 
  newMessage.value = '';
}
</script>  
  
<style scoped>  
.chat-container {  
  display: flex;  
  flex-direction: column;  
  height: 100%; /* 使聊天室充满整个视口高度 */  
  width: 100%;  
  max-width: 1200px; /* 可根据需要调整宽度 */  
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
  background-color: #faf8f8cd; /* 接收者消息背景色 */  
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