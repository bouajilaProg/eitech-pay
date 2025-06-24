<template>
  <div class="card mb-4 shadow-sm">
    <div class="card-header">
      <h2 class="card-title mb-0">Product Details</h2>
    </div>
    <div class="card-body" v-if="product">
      <!-- Product ID -->
      <div class="mb-4">
        <label class="form-label">Product ID</label>
        <p class="text-muted mb-0">{{ product.id || product.licenceId || product.subscriptionId }}</p>
      </div>

      <!-- Product Name -->
      <div class="mb-4">
        <label class="form-label">Product Name</label>
        <h3 class="mb-0">
          {{ product.name || product.licenceName || product.subscriptionName }}
        </h3>
      </div>

      <!-- Product Description -->
      <div class="mb-4">
        <label class="form-label">Description</label>
        <p class="mb-0">{{ product.description }}</p>
      </div>

      <!-- Optional Fields (conditionally shown) -->

      <!-- Max Devices (likely licence only) -->
      <div v-if="product.max_devices !== undefined" class="mb-4">
        <label class="form-label">Max Devices</label>
        <p class="mb-0">{{ product.max_devices }}</p>
      </div>

      <!-- Duration (likely subscription only) -->
      <div v-if="product.duration !== undefined" class="mb-4">
        <label class="form-label">Duration (days)</label>
        <p class="mb-0">{{ product.duration }}</p>
      </div>

      <!-- Grace Period (likely subscription only) -->
      <div v-if="product.gracePeriod !== undefined" class="mb-4">
        <label class="form-label">Grace Period (days)</label>
        <p class="mb-0">{{ product.gracePeriod }}</p>
      </div>

      <!-- Price (common to both) -->
      <div v-if="product.price !== undefined" class="mb-4">
        <label class="form-label">Price (TND)</label>
        <p class="mb-0">{{ product.price }}</p>
      </div>
    </div>
    <div v-else>
      <p>Loading product details...</p>
    </div>
  </div>
</template>

<script setup>


defineProps({
  product: {
    type: Object,
    required: true
  }
})


// console.log("ProductDetails component loaded", product)

</script>

<style scoped>
.card-title {
  font-size: 1.25rem;
}
</style>
