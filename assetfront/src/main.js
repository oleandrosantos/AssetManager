import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import '../public/src/style.css'
import 'vuetify/styles'
import { vuetify } from './plugins/vuetify'

createApp(App).use(router).use(vuetify).mount('#app')