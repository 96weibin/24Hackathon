<template>
  <div class="login-container">
    <img
      class="logo"
      src="Vue3-WebApplication/src/assets/icons/AspenLogo.svg"
    />
    <div class="login-content">
      <el-input class="username" v-model="username" placeholder="Name" />
      <el-input
        class="password"
        v-model="password"
        type="password"
        placeholder="Password"
        show-password
      />
      <el-button class="btn" type="primary" @click="onSubmit">Login</el-button>
      <div class="link">
        <el-button text="primary" disabled class="register">Register</el-button>
        <el-button text="primary" disabled>Forgot your password?</el-button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Ref, ref } from "vue";
import { Vue } from "vue-class-component";
import { useRouter } from "vue-router";
import { PeopleService } from "../../services/peopleService";
import { useStore } from "vuex";

export interface PeopleData {
  name: string;
  password: string;
}

export default class Login extends Vue {
  username: Ref<string> = ref("");
  password: Ref<string> = ref("");
  private router = useRouter();
  peopleService = new PeopleService();
  store = useStore();
  created() {
    localStorage.removeItem("username");
  }

  async onSubmit() {
    let request: PeopleData = {
      name: this.username as unknown as string,
      password: this.password as unknown as string,
    };
    const res = await this.peopleService.Login(request);
    if (res.data != null) {
      localStorage.setItem("username", this.username as unknown as string);
      localStorage.setItem("userRole", res.data.role as any);
      localStorage.setItem("userId", res.data.id as any);
      // this.store.commit('onChangeName', res.data.name);
      //  this.store.commit('onChangeRole', res.data.role);
      this.router.push(`/summary`);
    }
  }
}
</script>

<style lang="less">
.login-container {
  display: flex;
  flex-direction: column;
  .logo {
    align-self: center;
    width: 500px;
    margin-top: 120px;
  }
  .login-content {
    align-self: center;
    display: flex;
    flex-direction: column;
    width: 600px;
    margin-top: 30px;
    .username {
      height: 60px;
      margin: 20px 0px;
    }
    .password {
      height: 60px;
      margin: 20px 0px;
    }
    .btn {
      height: 60px;
      margin: 20px 0px;
    }
    .link {
      color: gray;
      margin-top: 10px;
      margin-bottom: 60px;
      display: flex;
      .register {
        margin-right: 50px;
      }
    }
  }
}
</style>
