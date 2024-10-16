import { createStore } from 'vuex'
import { Tabs } from '../services/homeService'

export default createStore({
  state: {
    userName: '',
    asideExpand: false,
    currentTab: Tabs.Summary,
    language: sessionStorage.getItem("localeLang") || window.navigator.language,
  },
  mutations: {
    onChangeAside(state) {
      // state.asideExpand = !state.asideExpand
    },
    onChangeTabs(state, arg) {
      state.currentTab = arg
    },
    onChangeWikiType(state, arg) {
      // state.wikiType = arg
    },
    onChangeLanguage(state, lang) {
      sessionStorage.setItem("localeLang", lang);
      state.language = lang;
    },
    onChangeName(state, newName) {
      state.userName = newName;
    }
  },
  actions: {
    asideAction(store) {
      store.commit("onChangeAside")
    },
    tabAction(store, arg) {
      store.commit("onChangeTabs", arg)
      console.log('tabAction')
    },
    wikiTypeAction(store, arg) {
      store.commit("onChangeWikiType", arg)
    }

  },
  modules: {
  },
  getters: {
    language: (state) => state.language,
  }
})

