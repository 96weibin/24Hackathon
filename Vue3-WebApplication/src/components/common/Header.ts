
import { Vue, Options } from "vue-class-component";

import { useStore } from "vuex";
import { useRouter } from "vue-router";

export default class Header extends Vue {
  private store = useStore();
  public userSortName: string = "";
  private lang: string;
  private searchValue = "";
  private router = useRouter();
  private searchedOptions = [];

  created() {
    this.getSortName();
  }

  private async getSortName() {
    // this.userData = (await this.userService.getLoginUser()).data;
    // this.store.commit('onChangeName', "yangsi")
    // if (this.userData.displayName) {
    //   let nameArray = this.userData.displayName.split(" ");
    //   if (nameArray.length == 1) {
    //     this.userSortName = nameArray[0][0];
    //   } else {
    //     this.userSortName = nameArray[0][0] + nameArray[1][0];
    //   }
    // } else {
    //   this.userSortName = ""
    // }
  }

}