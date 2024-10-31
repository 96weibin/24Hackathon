<template>
    <el-table :data="tableData" style="width: 100%">
      <el-table-column prop="variableName" label="Variables"/>
      <el-table-column prop="type" label="Type"/>
      <el-table-column prop="lowBound" label="Min"/>
      <el-table-column prop="highBound" label="Max"/>
      <el-table-column prop="margin" label="Margin"/>
    </el-table>
  </template>
  
  <script lang="ts" setup>
    const {tableData} = defineProps({tableData: Object})
  </script>