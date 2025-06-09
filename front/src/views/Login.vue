<template>
  <div class="page page-center d-flex flex-column">
    <div class="container-tight py-4">
      <div class="text-center mb-4">
        <a href="#" class="navbar-brand navbar-brand-autodark">
          <img src="https://tabler.io/static/logo.svg" height="36" alt="Logo" />
        </a>
      </div>

      <form class="card card-md" @submit.prevent="handleSubmit">
        <div class="card-body">
          <h2 class="card-title text-center mb-4">Login to your account</h2>

          <div class="mb-3">
            <label class="form-label">Email address</label>
            <input v-model="form.email" type="email" class="form-control" placeholder="Enter email" required />
          </div>

          <div class="mb-2">
            <label class="form-label">Password</label>
            <input v-model="form.password" type="password" class="form-control" placeholder="Password" required />
            <span class="form-label-description mb-4">
              <a href="#">I forgot password</a>
            </span>
          </div>

          <div class="mb-4">
            <label class="form-check">
              <input v-model="form.remember" type="checkbox" class="form-check-input" />
              <span class="form-check-label">Remember me</span>
            </label>
          </div>

          <div class="form-footer">
            <button type="submit" class="btn btn-primary w-100">Sign in</button>
          </div>
        </div>
      </form>

      <div class="text-center text-muted mt-3">
        Don't have account yet?
        <a href="register.html" tabindex="-1">Sign up</a>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { loginService } from '../services/login.service.ts'

const router = useRouter()

const form = reactive({
  email: '',
  password: '',
  remember: false
})

const handleSubmit = async () => {
  try {
    const { token, adminName } = await loginService.login({
      email: form.email,
      password: form.password
    })

    console.log('Logged in as:', adminName)

    if (form.remember) {
      // optional: save to localStorage (already handled in the service)
    }

    router.push('/dashboard')
  } catch (err) {
    console.error('Login failed:', err)
    alert('Invalid credentials.')
  }
}

onMounted(async () => {
  try {
    const session = await loginService.checkSession()
    console.log('Already logged in as:', session.adminName)
    router.push('/dashboard')
  } catch {
    // Not logged in — stay on login page
  }
})
</script>
