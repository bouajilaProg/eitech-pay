<template>
  <!-- Tiers Table -->
  <div class="card mb-4">
    <div class="card-header d-flex justify-content-between align-items-center">
      <h3 class="card-title mb-0">Tiers</h3>
    </div>
    <div class="table-responsive">
      <table class="table table-vcenter card-table table-hover">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Price ($)</th>
            <th>Duration (days)</th>
            <th>Grace Period</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="tier in tiers" :key="tier.id">
            <td><span class="text-muted">{{ tier.id }}</span></td>
            <td><span class="text-muted">{{ tier.tierName }}</span></td>
            <td><span class="text-muted">{{ tier.price }}</span></td>
            <td><span class="text-muted">{{ tier.duration }}</span></td>
            <td><span class="text-muted">{{ tier.gracePeriod }}</span></td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>

console.log("TiersTable component loaded", tiers)


defineProps({
  tiers: {
    type: Array,
    required: true
  }
})



</script>
