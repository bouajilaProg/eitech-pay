<template>
  <div class="card mb-4 shadow-sm">
    <div class="card-header">
      <h2 class="card-title mb-0">Edit Product Details</h2>
    </div>
    <div class="card-body">
      <!-- Product Name -->
      <div class="mb-4">
        <label class="form-label" for="product-name">Product Name</label>
        <input id="product-name" v-model="product.name" type="text" class="form-control"
          placeholder="Enter product name" />
      </div>

      <!-- Product ID -->
      <div class="mb-4">
        <label class="form-label" for="product-id">Product ID</label>
        <input id="product-id" v-model="product.id" type="text" class="form-control" placeholder="Enter product ID"
          readonly />
      </div>


      <!-- not sure if needed 
      <div class="mb-4">
        <label class="form-label" for="product-price">Price</label>
        <input id="product-price" v-model.number="product.price" type="number" step="0.01" min="0" class="form-control"
          placeholder="Enter price" />
      </div>

      <div class="mb-4">
        <label class="form-label" for="product-description">Description</label>
        <textarea id="product-description" v-model="product.description" class="form-control" rows="3"
          placeholder="Enter product description"></textarea>
      </div>

      <div class="form-check">
        <input id="product-available" type="checkbox" v-model="product.available" class="form-check-input" />
        <label class="form-check-label" for="product-available">Available</label>
      </div>
      -->


    </div>
  </div>
</template>

<script setup>
import { reactive } from 'vue'
import { productsData } from '../../temp-data.ts'

const product = reactive(
  productsData.find(p => p.id === 'learnito_web_1') || {
    name: 'Unknown Product',
    id: 'Unknown ID',
    price: 0,
    description: '',
    available: false
  }
)
</script>

<style scoped>
.card-title {
  font-size: 1.25rem;
}
</style>
