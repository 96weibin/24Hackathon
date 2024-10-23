<template>
  <div class="header-row">
    <img
      class="logo"
      src="Vue3-WebApplication/src/assets/icons/AspenLogo.svg"
      @click="onSummary"
    />
    <div class="user">
      <div class="user-icon">{{ userSortName }}</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue } from "vue-class-component";
import { useRouter } from "vue-router";
export default class Header extends Vue {
  private router = useRouter();
  userSortName = "ABC";
  onSummary() {
    this.router.push({ path: "/summary" });
  }
}
</script>

<style lang="less">
.header-row {
  width: 100%;
  height: 48px;
  line-height: 48px;
  padding: 0;
  border-bottom: 1px solid #e5e5e5;
  display: flex;
  justify-content: space-between;
  align-items: center;
  .logo {
    height: 30px;
    margin-left: 20px;
    cursor: pointer;
  }
  .user {
    width: 48px;
    height: 48px;
    margin-right: 20px;
    display: flex;
    justify-content: center;
    align-items: center;
    .user-icon {
      width: 30px;
      height: 30px;
      line-height: 30px;
      text-align: center;
      font-weight: bold;
      color: #e5e5e5;
      background: #0078d4;
      border-radius: 50%;
    }
  }
}
</style>
