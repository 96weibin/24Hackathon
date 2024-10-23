<template>
  <el-container class="screen-frame">
    <el-header class="screen-header">
      <Header></Header>
    </el-header>
    <el-container style="height: calc(100% - 48px); overflow: hidden">
   
      <el-main class="screen-main">
        <el-scrollbar class="screen-main-scroll"
          ><router-view></router-view></el-scrollbar>
      </el-main>
    </el-container>
  </el-container>
</template>

<script setup lang="ts">
import Header from "../components/common/Header.vue";
</script>
