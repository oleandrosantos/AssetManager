<template>
  <div class="container-sm d-flex justify-content-center ">
    <main class="form-signin">
      <form>
        <h1 class="text-center">Login</h1>
        <div class="form-floating">
          <input type="email" class="form-control" id="floatingInput" placeholder="name@example.com">
          <label for="floatingInput">Email</label>
        </div>
        <div class="form-floating">
          <input type="password" class="form-control" id="floatingPassword" placeholder="Password">
          <label for="floatingPassword">Password</label>
        </div>

        <div class="checkbox mb-3">
          <label>
            <input type="checkbox" value="remember-me"> Remember me
          </label>
        </div>
        <button class="w-100 btn btn-lg btn-primary" type="submit">Entrar</button>
        <p class="text-center">&copy; 2021</p>
      </form>
  </main>
  </div>
</template>

<script>
export default {
  name: 'LoginApp'
}
</script>

<style>

</style>