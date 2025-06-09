<template>
  <div class="page-wrapper">
    <div class="page-body">
      <div>
        <div class="row row-deck row-cards mb-4">
          <StatCard title="New Users" :value="newUsers" />
          <StatCard title="Revenue" :value="revenue" />
          <StatCard title="Orders" :value="orders" />
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
import { ref, onMounted } from 'vue';
import apiHandler from '@/lib/apiHandler';
import StatCard from '@/components/StatCard.vue';
import RevenueChart from '@/components/RevenueChart.vue';
import { BACKEND_URL } from '../static-default.ts';

const newUsers = ref(0);
const revenue = ref('$0');
const orders = ref(0);

onMounted(async () => {
  try {
    const data = await apiHandler.getDashboardStats();
    newUsers.value = data.newUsers;
    revenue.value = data.revenue;
    orders.value = data.orders;
  } catch (error) {
    console.error('Failed to load dashboard data:', error);
  }
});

</script>
