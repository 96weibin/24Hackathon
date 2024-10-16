import {
  createI18n
} from 'vue-i18n';
import messages from '../locales/index';

//注册i8n实例并引入语言文件
const localeData = {
  legacy: false, // composition API
  locale: sessionStorage.getItem("localeLang") || window.navigator.language,
  messages,
  globalInjection: true,
  datetimeFormats: {
    'en': {
      short: {
        year: 'numeric',
        month: 'short',
        day: 'numeric'
      },
      long: {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        weekday: 'long',
        hour: 'numeric',
        minute: 'numeric'
      }
    },
    'zh': {
      short: {
        year: 'numeric',
        month: 'short',
        day: 'numeric'
      },
      long: {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        weekday: 'long',
        hour: 'numeric',
        minute: 'numeric',
        hour12: true
      }
    }
  },
  isGlobal: true,
  getDateTimeFormat: (locale) => {
    return this.datetimeFormats[locale]
  }
}
// setup i18n instance with glob
export function setupI18n(app) {
  const i18n = createI18n(localeData);
  app.use(i18n);
}