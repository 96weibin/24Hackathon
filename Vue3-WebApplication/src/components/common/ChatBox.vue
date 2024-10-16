<template>  
    <div class="chat-container">  
      <div class="messages">  
        <div  
          v-for="(message, index) in messages"  
          :key="index"  
          :class="['message', message.isSender ? 'sender' : 'receiver']"  
        >  
          <span>{{ message.content }}</span>  
        </div>  
      </div>  
      <div class="input-container">  
        <el-input  
          v-model="newMessage"  
          placeholder="输入消息"  
          class="input-box"  
          @keyup.enter="sendMessage"  
        />  
        <el-button type="primary" @click="sendMessage">发送</el-button>  
      </div>  
    </div>  
  </template>  
    
  <script lang="ts">  
  import { defineComponent, ref } from 'vue';  
  import { ElInput, ElButton } from 'element-plus';  
  import 'element-plus/dist/index.css';  
    
  interface Message {  
    content: string;  
    isSender: boolean; // true 表示发送者（右边），false 表示接收者（左边）  
  }  
    
  export default defineComponent({  
    name: 'ChatBox',  
    components: {  
      ElInput,  
      ElButton,  
    },  
    setup() {  
      const messages: Message[] = ref([  
        // 初始化一些模拟消息  
        { content: '你好，我是接收者！', isSender: false },  
        { content: '你好，我是发送者！', isSender: true },  
        // ... 可以继续添加更多模拟消息  
      ]);  
    
      const newMessage = ref<string>('');  
    
      const sendMessage = () => {  
        if (newMessage.value.trim()) {  
          messages.value.push({ content: newMessage.value, isSender: true });  
          // 这里可以添加逻辑来处理接收者的回复，比如从服务器获取数据  
          // 为了演示，我们简单地模拟一个接收者回复  
          setTimeout(() => {  
            const receiverMessage = `接收者回复: ${newMessage.value.endsWith('?') ? '是的' : '不是'}`;  
            messages.value.push({ content: receiverMessage, isSender: false });  
          }, 1000); // 延迟1秒模拟接收者回复  
          newMessage.value = '';  
        }  
      };  
    
      return {  
        messages,  
        newMessage,  
        sendMessage,  
      };  
    },  
  });  
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