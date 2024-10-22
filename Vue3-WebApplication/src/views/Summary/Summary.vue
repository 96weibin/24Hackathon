<template>
  <ChatBox></ChatBox>
</template>
<script lang="ts" setup>
  import ChatBox from '../../components/common/ChatBox.vue';
</script>

<style lang="less">
.screen-view {
  display: flex;
  flex-direction: column;
  .screen-view_header {
    display: flex;
    align-items: center;
    justify-content: flex-end;
    .title-text {
      padding-left: 30px;
      margin: auto;
    }
  }
  .menu {
    display: flex;
    align-items: center;
    margin-left: 100px;
    justify-content: center;
    .search {
      display: flex;
      align-items: center;
      .el-form-item {
        margin-bottom: 0px;
      }
      .search-input {
        width: 500px;
      }
      .search-button {
        margin-left: 10px;
        margin-right: 30px;
      }
    }
    .el-menu {
      width: 230px;
      font-weight: bolder;
    }
    .el-menu--horizontal {
      border-bottom: none;
    }
    .el-menu--horizontal > .el-sub-menu.is-active .el-sub-menu__title {
      border-bottom: none;
    }
  }
  .background {
    display: flex;
    margin-top: 20px;
    justify-content: center;
    img {
      height: 760px;
      width: 1310px;
    }
  }
}

#webchat {
  width: 100%;
  height: 600px;
}

.postQbtn {
  color: #fff;
  border: none;
  border-radius: 4px;
  background: rgb(103, 194, 58);
  height: 40px;
}
.postQbtn:hover {
  background: rgba(103, 194, 58, 0.7);
}
.my-header {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
</style>
