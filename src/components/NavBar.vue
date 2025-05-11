<template>
  <header class="navbar navbar-expand-md navbar-light d-print-none">
    <div class="container-xl">
      <RouterLink to="/" class="navbar-brand">
        <img src="https://tabler.io/static/logo.svg" height="36" alt="Logo" />
      </RouterLink>
      <div class="navbar-nav flex-row order-md-last">
        <div class="nav-item dropdown">
          <a href="#" class="nav-link d-flex lh-1 text-reset p-0" data-bs-toggle="dropdown" aria-label="Open user menu">
            <span class="avatar avatar-sm"
              style="background-image: url(https://tabler.io/static/avatars/000m.jpg)"></span>
          </a>
          <div class="dropdown-menu dropdown-menu-end dropdown-menu-arrow">
            <RouterLink to="/profile" class="dropdown-item">Profile</RouterLink>
            <RouterLink to="/settings" class="dropdown-item">Settings</RouterLink>
            <div class="dropdown-divider"></div>
            <RouterLink to="/logout" class="dropdown-item">Logout</RouterLink>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>

<script setup>
import { RouterLink } from 'vue-router';
</script>
