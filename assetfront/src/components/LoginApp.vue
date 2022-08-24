<template>
  <div class="lg">
    <main class="form-signin">
      <form>
        <h1 class="h3 mb-3 fw-normal">Login</h1>
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
        <p class="mt-5 mb-3 text-muted">&copy; 2017–2021</p>
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