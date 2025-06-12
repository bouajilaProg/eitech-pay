<template>
  <div class="card mb-4 shadow-sm">
    <div class="card-header">
      <h2 class="card-title mb-0">Product Details</h2>
    </div>
    <div class="card-body">
      <!-- Product ID -->
      <div class="mb-4">
        <label class="form-label">Product ID</label>
        <p class="text-muted mb-0">{{ productDetails.id }}</p>
      </div>

      <!-- Product Name -->
      <div class="mb-4">
        <label class="form-label">Product Name</label>
        <h3 class="mb-0">{{ productDetails.name }}</h3>
      </div>

      <!-- Product Description -->
      <div class="mb-4">
        <label class="form-label">Description</label>
        <p class="mb-0">{{ productDetails.description }}</p>
      </div>

      <!-- Optional Fields -->
      <div v-if="productDetails.max_devices" class="mb-4">
        <label class="form-label">Max Devices</label>
        <p class="mb-0">{{ productDetails.max_devices }}</p>
      </div>

      <div v-if="productDetails.duration" class="mb-4">
        <label class="form-label">Duration (days)</label>
        <p class="mb-0">{{ productDetails.duration }}</p>
      </div>

      <div v-if="productDetails.grace_period" class="mb-4">
        <label class="form-label">Grace Period (days)</label>
        <p class="mb-0">{{ productDetails.grace_period }}</p>
      </div>

      <div v-if="productDetails.price" class="mb-4">
        <label class="form-label">Price ($)</label>
        <p class="mb-0">{{ productDetails.price }}</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const productDetails = ref({})

onMounted(() => {
  const id = route.params.product_id

  if (id === '1') {
    productDetails.value = {
      id: 'prod_1',
      name: 'name_1',
      description: 'description_1',
      max_devices: 5,
      duration: 30,
      grace_period: 7,
      price: 20,
    }
  } else if (id === '2') {
    productDetails.value = {
      id: 'prod_2',
      name: 'name_2',
      description: 'description_2',
    }
  } else {
    productDetails.value = {
      id: 'unknown',
      name: 'Unknown Product',
      description: 'No description available.',
    }
  }
})
</script>

<style scoped>
.card-title {
  font-size: 1.25rem;
}
</style>
