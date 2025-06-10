<template>
  <div class="card mb-4 shadow-sm">
    <div class="card-header">
      <h2 class="card-title mb-0">Product Details</h2>
    </div>
    <div class="card-body">
      
            <!-- Product ID -->
            <div class="mb-4">
              <label class="form-label">Product ID</label>
              <p class="text-muted mb-0">{{ product.id }}</p>
            </div>
      <!-- Product Name -->
      <div class="mb-4">
        <label class="form-label">Product Name</label>
        <h3 class="mb-0">{{ product.name }}</h3>
      </div>

      <!-- Product Description -->
      <div class="mb-4">
        <label class="form-label">Description</label>
        <p class="mb-0">{{ product.description }}</p>
      </div>

      <!-- Product Type -->
      <div class="mb-4">
        <label class="form-label">Type</label>
        <p class="mb-0">{{ product.productType == 0 ? 'License' : 'Subscription' }}</p>
      </div>

    </div>
  </div>
</template>


<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { productService } from '@/services/product.service.ts'

const route = useRoute()
const product = ref({
  name: '',
  id: '',
  description: '',
  type: '',
})

async function loadProduct() {
  const id = route.params.product_id
  try {
    const data = await productService.getById(id)
    product.value = data
    console.log('Product loaded:', product.value)
  } catch (error) {
    console.error('Failed to fetch product:', error)
    product.value = {
      name: 'Product not found',
      id: id,
      description: '',
      type: '',
    }
  }
}

onMounted(loadProduct)
</script>



<style scoped>
.card-title {
  font-size: 1.25rem;
}
</style>
