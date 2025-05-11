<template>
  <div class="page-wrapper">
    <div class="page-body">
      <div class="container-xl">
        <div class="row row-deck row-cards mb-4">
          <StatCard title="New Users" :value="1523" />
          <StatCard title="Revenue" value="$12,340" />
          <StatCard title="Orders" :value="345" />
        </div>
        <div class="row row-cards">
          <div class="col-12">
            <RevenueChart />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import StatCard from '@/components/StatCard.vue';
import RevenueChart from '@/components/RevenueChart.vue';
</script>
