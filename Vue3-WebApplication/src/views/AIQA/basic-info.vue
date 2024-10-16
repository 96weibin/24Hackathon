<template>
    <div class="info">
        <span>
        <el-avatar class="user-icon">{{name.substring(0,2).toUpperCase()}}</el-avatar>
        <strong class="user-name">{{name}}</strong>
        </span>
        <span class="create-date">{{time}}</span>
    </div>
</template>
<script lang="ts">
    export default {
        name: 'basicInfo',
        props:{
            name:String,
            time:String,
        }
    }
</script>
<style scoped>
.info{
      display: flex;
      justify-content: space-between;
      flex-direction: row;
      align-items: center;
      color:#0078d4;
      height:45px;
    }
.user-icon{
    width: 45px;
    height: 45px;
    line-height: 45px;
    text-align: center;
    font-weight: bold;
    color: #E5E5E5;
    background: #0078d4;
    border-radius: 50%;
    font-size: 18px;
    }
.user-name{
      margin-left:10px;
    }
</style>