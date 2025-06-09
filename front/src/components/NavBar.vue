<template>
  <header class="navbar navbar-expand-md navbar-light d-print-none">
    <div class="container-xl">
      <RouterLink to="/" class="navbar-brand">
        <h2>EItech</h2>
      </RouterLink>

      <div v-if="route.path !== '/login'" class="navbar-nav flex-row order-md-last">
        <div class="nav-item d-none d-md-flex me-3">
          <RouterLink to="/docs" class="nav-link">
            <strong>Docs</strong>
          </RouterLink>
        </div>

        <div class="nav-item d-none d-md-flex me-3">
          <RouterLink to="/settings" class="btn btn-outline-secondary">
            <CgProfile style="margin-right: 4px;" />
            Admin
          </RouterLink>
        </div>
      </div>
    </div>
  </header>
</template>

<script setup>
import { RouterLink, useRoute } from 'vue-router'
import { CgProfile } from 'vue-icons-plus/cg'

const route = useRoute()
</script>
