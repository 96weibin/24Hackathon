<template>
  <router-view></router-view>
</template>

<script setup lang="ts">
import "./assets/icons.less";
import axios from "axios";
import Header from "./components/common/Header.vue";
import Aside from "./components/Aside/Aside.vue";
</script>

<style lang="less">
html,
body {
  height: 100%;
  margin: 0;
}
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  position: relative;
  top: 0;
  left: 0;
  width: 100%;
  /*min-width: 1440px;*/
  height: 100%;
  margin: 0;
  padding: 0;
  a {
    text-decoration: none;
  }
  .router-link-active {
    text-decoration: none;
  }
}

.screen-frame {
  width: 100%;
  height: 100%;
  overflow: hidden;

  .screen-header {
    padding: 0;
    height: 48px;
  }

  // .screen-aside {
  //   width: fit-content !important;
  //   height: 100%;
  // }

  .screen-main {
    color: #2c3e50;
    height: 100%;
    padding-right: 0; //default 20px

    .screen-main-scroll {
      padding-right: 15px;
    }
    .el-scrollbar__view {
      height: 100%;
    }
  }
}

.screen-view {
  .screen-view_header {
    margin: 0px 0px 10px 0px;
  }
}
</style>
