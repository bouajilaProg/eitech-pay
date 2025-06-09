<template>
  <div class="card mb-4 shadow-sm">
    <div class="card-header">
      <h2 class="card-title mb-0">Product Details</h2>
    </div>
    <div class="card-body">
      <!-- Product Name -->
      <div class="mb-4">
        <label class="form-label">Product Name</label>
        <h3 class="mb-0">{{ product.name }}</h3>
      </div>

      <!-- Product ID -->
      <div class="mb-4">
        <label class="form-label">Product ID</label>
        <p class="text-muted mb-0">ID: {{ product.id }}</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive } from 'vue'
import {productsData} from '../../temp-data.ts'

const product = reactive(
productsData.find(p => p.id === 'learnito_web_1') || {
  name: 'Unknown Product',
  id: 'Unknown ID'
}
)
</script>

<style scoped>
.card-title {
  font-size: 1.25rem;
}
</style>
