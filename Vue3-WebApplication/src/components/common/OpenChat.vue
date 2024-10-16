<template>
  <div>
    <el-button class="openChatButton" icon="ChatLineRound" type="primary" @click="openChatDrawer" round>open chat</el-button>
    <el-drawer v-model="drawer" title="Chat here">
        <ChatBox></ChatBox>
    </el-drawer>
  </div>
</template>

<script lang="ts">
import { ref } from 'vue'
import { Options, Vue } from 'vue-class-component';
import ChatBox from './ChatBox.vue';
@Options({
    components: {
        ChatBox,
    }
})
export default class OpenChat extends Vue{
    private drawer = false
    private openChatDrawer() {
        this.drawer = !this.drawer
    }
}
</script>

<style scoped>
.openChatButton{
    position: absolute;
    right: 200px;
    bottom: 200px;
}
</style>
