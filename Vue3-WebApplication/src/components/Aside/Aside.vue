<template>
  <el-menu
    default-active="1"
    class="el-menu-vertical-demo"
    :collapse="isCollapse"
    @select="handleSelect"
    background-color="#EAEAEA"
    text-color="#171717"
    active-text-color="#141414"
  >
    <el-sub-menu :index="Tabs.MyWork.toString()">
      <template #title>
        <el-icon><Suitcase /></el-icon>
        <span>My Work</span>
      </template>
      <el-menu-item-group title="My Work">
        <el-menu-item :index="Tabs.Summary.toString()">
          <el-icon><Location /></el-icon>
          <span>Summary</span>
        </el-menu-item>
        <el-menu-item :index="Tabs.Charts.toString()">
          <el-icon><Document /></el-icon>
          <span>Charts</span>
        </el-menu-item>
        <el-menu-item :index="Tabs.Grid.toString()">
          <el-icon><DataAnalysis /></el-icon>
          <span>Grid</span>
        </el-menu-item>
        <el-menu-item :index="Tabs.MyInfo.toString()">
          <el-icon><User /></el-icon>
          <span>My Info</span>
        </el-menu-item>  
    
      </el-menu-item-group>
    </el-sub-menu>
  </el-menu>
</template>

<script lang="ts">
import Aside from './Aside'
export default Aside
</script>

<style lang="less">
.el-menu-vertical-demo{
  height: 100%;
}
.el-menu-vertical-demo:not(.el-menu--collapse) {
  width: 200px;
  min-height: 400px;
}
</style>
