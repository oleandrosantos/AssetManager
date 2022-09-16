<template>
  <div class="container-sm d-flex justify-content-center ">
    <main class="form-signin">
      <form @submit.prevent="logar()">
        <h1 class="text-center">Login</h1>
        <div class="form-floating">
          <input type="email" v-model="usuario.email" class="form-control" id="floatingInput" placeholder="name@example.com">
          <label for="floatingInput">Email</label>
        </div>
        <div class="form-floating">
          <input type="password" v-model="usuario.password" class="form-control" id="floatingPassword" placeholder="Password">
          <label for="floatingPassword">Password</label>
        </div>

        <div class="checkbox mb-3">
          <label>
            <input type="checkbox" value="remember-me"> Remember me
          </label>
        </div>
        <button class="w-100 btn btn-lg btn-primary" type="submit">Entrar</button>
        <p v-if="mensagemErro"> {{ mensagemErro }} </p>
        <p class="text-center">&copy; 2022</p>
      </form>
  </main>
  </div>
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
    mensagemErro: ''
  }),
  methods:{
    async logar(){
      let result = await signIn(this.usuario.email, this.usuario.password);
      if(result === true){
        this.$router.push('/home');
        return;
      }
      this.mensagemErro= result.mensagem;
    }
  }
}
</script>

<style>

</style>