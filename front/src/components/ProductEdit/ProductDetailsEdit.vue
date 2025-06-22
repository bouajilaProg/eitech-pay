<template>
  <div class="card mb-4 shadow-sm">
    <div class="card-header">
      <h2 class="card-title mb-0">Edit Product Details</h2>
    </div>
    <div class="card-body">

      <!-- Product Name -->
      <div class="mb-4">
        <label class="form-label" for="product-name">Product Name</label>
        <input id="product-name" v-model="product.name" type="text" class="form-control" placeholder="Enter product name" />
      </div>

      <!-- Description -->
      <div class="mb-4">
        <label class="form-label" for="product-description">Description</label>
        <textarea id="product-description" v-model="product.description" class="form-control" rows="3"
          placeholder="Enter product description"></textarea>
      </div>

      <!-- Product Type -->
      <div class="mb-4">
        <label class="form-label" for="product-type">Type</label>
        <select id="product-type" v-model="product.productType" class="form-control">
          <option value="0">License</option>
          <option value="1">Subscription</option>
        </select>
      </div>

     
    </div>
  </div>
</template>

<script setup>
import { reactive, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { productService } from '@/services/product.service.ts'

// Get product_id from route
const route = useRoute()
const productId = route.params.product_id

// Reactive product object
const product = reactive({
  name: '',
  description: '',
  productType: ''
})

async function loadProduct() {
  try {
    const data = await productService.getById(productId)
    Object.assign(product, data)
  } catch (err) {
    console.error('Failed to fetch product:', err)
  }
}

// Export product for parent to access it
defineExpose({ product })

onMounted(loadProduct)
</script>


<style scoped>
.card-title {
  font-size: 1.25rem;
}
</style>
