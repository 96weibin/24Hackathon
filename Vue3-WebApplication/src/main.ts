import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import installElementPlus from './plugins/element'
import Markdown from 'vue3-markdown-it';
const app = createApp(App)
app.directive('focus', {  // custom directive named v-focus,  use to focus on input
    mounted(el) {
        el.querySelector('input').focus();

    }
})
installElementPlus(app)
app.use(store).use(router).use(Markdown).mount('#app')