<template>
  <div class="card mb-4 shadow-sm">
    <div class="card-header">
      <h2 class="card-title mb-0">Edit Product Details</h2>
    </div>
    <div class="card-body" v-if="product">
      <!-- Product ID
      <div class="mb-4">
        <label class="form-label" for="product-id">Product ID</label>
        <input
          id="product-id"
          v-model="productId"
          type="text"
          class="form-control"
          placeholder="Enter product ID"
        />
      </div> -->

      <!-- Product Name -->
      <div class="mb-4">
        <label class="form-label" for="product-name">Product Name</label>
        <input
          id="product-name"
          v-model="productName"
          type="text"
          class="form-control"
          placeholder="Enter product name"
        />
      </div>

      <!-- Description -->
      <div class="mb-4">
        <label class="form-label" for="product-description">Description</label>
        <textarea
          id="product-description"
          v-model="product.description"
          class="form-control"
          rows="3"
          placeholder="Enter product description"
        ></textarea>
      </div>

      <!-- Max Devices (Licence Only) -->
      <div v-if="product.maxDevices !== undefined" class="mb-4">
        <label class="form-label" for="max-devices">Max Devices</label>
        <input
          id="max-devices"
          v-model.number="product.maxDevices"
          type="number"
          class="form-control"
          placeholder="Enter max devices"
        />
      </div>

      <!-- Duration (Licence Only) -->
      <div v-if="product.duration !== undefined" class="mb-4">
        <label class="form-label" for="duration">Duration (days)</label>
        <input
          id="duration"
          v-model.number="product.duration"
          type="number"
          class="form-control"
          placeholder="Enter duration in days"
        />
      </div>

      <!-- Grace Period (Licence Only) -->
      <div v-if="product.gracePeriod !== undefined" class="mb-4">
        <label class="form-label" for="grace-period">Grace Period (days)</label>
        <input
          id="grace-period"
          v-model="product.gracePeriod"
          type="text"
          class="form-control"
          placeholder="Enter grace period in days"
        />
      </div>

      <!-- Price (Licence Only) -->
      <div v-if="product.price !== undefined" class="mb-4">
        <label class="form-label" for="price">Price (TND)</label>
        <input
          id="price"
          v-model.number="product.price"
          type="number"
          step="0.01"
          class="form-control"
          placeholder="Enter price"
        />
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive, computed, defineExpose } from 'vue'

const props = defineProps({
  product: {
    type: Object,
    required: true,
  },
})

// Create editable copy of the product
const product = reactive({ ...props.product })

// Computed property for editable ID (either licenceId or subscriptionId)
const productId = computed({
  get() {
    return product.licenceId || product.subscriptionId || ''
  },
  set(value) {
    if ('licenceId' in product) {
      product.licenceId = value
    } else if ('subscriptionId' in product) {
      product.subscriptionId = value
    }
  }
})

// Computed property for editable name
const productName = computed({
  get() {
    return product.licenceName || product.subscriptionName || ''
  },
  set(value) {
    if ('licenceName' in product) {
      product.licenceName = value
    } else if ('subscriptionName' in product) {
      product.subscriptionName = value
    }
  }
})

defineExpose({ product })
</script>

<style scoped>
.card-title {
  font-size: 1.25rem;
}
</style>
