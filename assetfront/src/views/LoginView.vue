<template>
  <v-sheet class="d-flex bg-indigo py-12 h-100 justify-center align-center" rounded>
    <v-card class="mx-2" width="500px">
      <v-form
      v-model="form"
      lazy-validation
      @submit.prevent="logar"
      >
        <v-container fluid>
          <v-row  class="mb-5"> 
            <v-col align-self="center">
              <p class="text-h4 text-center">Login</p>
            </v-col>
          </v-row>
          <v-row > 
            <v-col align-self="center">
              <v-text-field label="Email address" 
              v-model="usuario.email" 
              :rules="rules.emailRules"
              type="email" 
              placeholder="usuario@mail.com" 
              required
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row > 
            <v-col align-self="center">
              <v-text-field
              v-model="usuario.password"
              :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
              :rules="[rules.required, rules.min]"
              :type="showPassword ? 'text' : 'password'"
              name="input-10-1"
              label="Senha"
              hint="At least 8 characters"
              counter
              @click:append="showPassword = !showPassword"
              ></v-text-field>
            </v-col>  
          </v-row>
          <v-row class="mt-3 mb-2"> 
            <v-col>
              <v-btn
                :disabled="!form"
                :loading="loading"
                block
                color="success"
                size="large"
                type="submit"
                variant="elevated"
              >
                Sign In
              </v-btn>
            </v-col>  
          </v-row>
          <v-row  class="mb-5"> 
            <v-col align-self="center">
              <p class="text-center text-body-2">Ainda não tem sua conta? <a href="/signup/">Cadastrar</a></p>
            </v-col>
          </v-row>
        </v-container>
      </v-form>
    </v-card>
  </v-sheet>
</template>

<script>
import { signIn } from '../auth';

export default {
  name: 'LoginApp',
  data: () => ({
    usuario:{
      email: '',
      password: '',
    },
    form: false,
    loading: false,
    showPassword: false,
    mensagemErro: '',
    rules: {
          required: value => !!value || 'Required.',
          min: v => v.length >= 8 || 'Minimo de 8 caracteres',
          emailRules: [
            v => !!v || 'E-mail é requerido',
            v => /.+@.+\..+/.test(v) || 'E-mail não é valido',
          ],
        },
  }),
  methods:{
    async logar(){
      this.loading = true;
      let result = await signIn(this.usuario.email, this.usuario.password);
      if(result === true){
        this.$router.push('/home');
        return;
      }
      this.loading = false;
      console.log(result.mensagem);
      this.mensagemErro= result.mensagem;
    }
  }
}
</script>

<style>

</style>
