<template>
  <div class="home-main">
    <el-row class="btns-row">
      <el-col :span="24" class="btns">
        <span>
          <router-link to="/userControl">
        <el-button type="primary" round>用户管理</el-button>
      </router-link>
          <router-link to="/space" class="space-link">
            <i class="svg-icon-space" style="width:16px; height:16px"></i>空间
          </router-link>
          <router-link to="mine" class="mine-link">
            <i class="svg-icon-mine" style="width:16px; height:16px"></i>我的
          </router-link>
        </span>
      </el-col>
    </el-row>
    <el-row class="search-tags-row">
      <el-col :span="14" :offset="4" >
        <el-row >
          <el-col class="search-col">
            <el-autocomplete class="search-inp"
              clearable 
              v-model="state4"
              :suffix-icon = "Search"
              :fetch-suggestions="querySearchAsync"
              placeholder="请输入内容"
              @select="handleSelect"
              size="large"
            ></el-autocomplete>
          </el-col>
        </el-row>
        <el-row>
          <el-col class="tags-col">
            <div class="hot-tag">
              <i class="svg-icon-tag" style="width:20px; height:20px"></i>
              <router-link class="tags-label" :to="'/tag/' + tag.id" v-for="tag in tags" :key="tag.id">{{tag.name}}</router-link >
            </div>
          </el-col>
        </el-row>
      </el-col>
      <el-col :span="6">
        <img src="@/assets/imgs/excel.png" alt="">
      </el-col>
    </el-row>
  </div>
</template>

<script setup>
    let tags = [
      {
        id: 1,
        name: '热门标题'
      },
      {
        id: 2,
        name: '自动化运维'
      },
      {
        id: 3,
        name: 'AI智能化'
      },
      {
        id: 4,
        name: '自动化运维'
      },
      {
        id: 5,
        name: 'AI智能化'
      },
      {
        id: 6,
        name: '服务器宕机'
      }
    ]
</script>

<style lang="less" scoped>
.home-main{
  width: 100%;
  height: 300px;
  background: rgba(0, 0, 0, .38);
  border-radius: 5px;
  .btns-row{
    height: 40px;
    padding-top: 20px;
    .btns{
      display: flex;
      justify-content: flex-end;
      padding-right: 20px;
      font-size: 16px;
      height: 20px;
      .space-link,.mine-link{
        width:20px;
        height:20px;
        color:#FFF;
        vertical-align: text-bottom;
        padding: 0 5px;
      }
    }
  }
  .search-tags-row{
    height: 260px;
    .search-col{
      // height: 100%;
      display: flex;
      justify-content: center;
      /deep/.search-inp{
        width: 700px;
        margin-top: 90px;
      }
    }
    .tags-col{
      margin-top: 10px;
      width: 100%;
      display: flex;
      justify-content: center;
      .hot-tag{
        width: 700px;
        height: 40px;
        color: #FFF;
        .tags-label{
          color: #FFF;
          font-size: 20px;
          margin-right: 20px;
        }
      }
    }
  }
}
</style>
