<template>
  <!-- Tiers Table -->
  <div class="card mb-4">
    <div class="card-header d-flex justify-content-between align-items-center">
      <h3 class="card-title mb-0">Subscription Tiers</h3>
    </div>
    <div class="table-responsive">
      <table class="table table-vcenter card-table table-hover">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Duration (days)</th>
            <th>Grace Period (days)</th>
            <th>Price (TND)</th>
          </tr> 
        </thead>
        <tbody>
          <tr v-for="tier in tiers" :key="tier.tierId">
            <td><span class="text-muted">{{ tier.tierId }}</span></td>
            <td><span class="text-muted">{{ tier.tierName }}</span></td>
            <td><span class="text-muted">{{ tier.duration }}</span></td>
            <td><span class="text-muted">{{ tier.gracePeriod }}</span></td>
            <td><span class="text-muted">{{ tier.price }}</span></td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
defineProps({
  tiers: {
    type: Array,
    required: true
  }
})
</script>
