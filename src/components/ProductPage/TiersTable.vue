<template>
  <!-- Tiers Table -->
  <div class="card mb-4">
    <div class="card-header d-flex justify-content-between align-items-center">
      <h3 class="card-title mb-0">Tiers</h3>
      <!-- Removed the New Tier button and Edit Mode toggle button -->
    </div>
    <div class="table-responsive">
      <table class="table table-vcenter card-table table-hover">
        <thead>
          <tr>
            <th>ID</th>
            <th>Price</th>
            <th>Duration</th>
            <th>Sales</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(tier, index) in tiers" :key="index">
            <td><span class="text-muted">{{ tier.id }}</span></td>
            <td><span class="text-muted">{{ tier.price }}</span></td>
            <td><span class="text-muted">{{ tier.duration }}</span></td>
            <td><span class="text-muted">{{ tier.sales }}</span></td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { productsData } from '../../temp-data.ts'

const tiers = ref(
  productsData.find(p => p.id === 'learnito_web_1')?.tiers || [
    { id: 'tier_1', price: '$10', duration: '1 month', sales: 100 },
    { id: 'tier_2', price: '$25', duration: '3 months', sales: 200 },
    { id: 'tier_3', price: '$50', duration: '6 months', sales: 300 }
  ]
)
</script>
