<template>
  <div class="card">
    <div class="card-header">
      <div class=" flex w-full justify-between">
        <h3 class="card-title">Revenue</h3>
        <div class="flex">
          <div class="me-3">
            <select v-model="selectedProduct" class="form-select form-select-md">
              <option value="all">All Products</option>
              <option value="subscription">Subscriptions</option>
              <option value="license">Licenses</option>
            </select>
          </div>
          <div class="me-3">
            <select v-model="selectedPeriod" class="form-select form-select-md">
              <option value="monthly">Monthly</option>
              <option value="quarterly">Quarterly</option>
              <option value="yearly">Yearly</option>
            </select>
          </div>
          <div>
            <select v-model="selectedRange" class="form-select form-select-md">
              <option value="7">Last 7 months</option>
              <option value="12">Last 12 months</option>
              <option value="24">Last 24 months</option>
            </select>
          </div>
        </div>
      </div>
    </div>
    <div class="card-body h-96">
      <Bar :data="chartData" :options="chartOptions" />
    </div>
  </div>
</template>

<script setup>
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale,
} from 'chart.js';
import { Bar } from 'vue-chartjs';
import { ref, computed } from 'vue';

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

const selectedProduct = ref('all');
const selectedPeriod = ref('monthly');
const selectedRange = ref('7');

const chartData = computed(() => {
  // Modify data based on filters
  // This is a simplified example - in a real app you'd likely fetch this data
  // based on the selected filters

  let labels, netProfit, revenue;

  if (selectedPeriod.value === 'monthly') {
    labels = ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug'];

    if (selectedProduct.value === 'all') {
      netProfit = [30, 40, 35, 50, 49, 60, 70];
      revenue = [40, 50, 45, 60, 59, 70, 80];
    } else if (selectedProduct.value === 'subscription') {
      netProfit = [20, 25, 22, 35, 36, 40, 45];
      revenue = [25, 30, 28, 42, 43, 48, 52];
    } else { // license
      netProfit = [10, 15, 13, 15, 13, 20, 25];
      revenue = [15, 20, 17, 18, 16, 22, 28];
    }
  } else if (selectedPeriod.value === 'quarterly') {
    labels = ['Q1', 'Q2', 'Q3', 'Q4'];

    if (selectedProduct.value === 'all') {
      netProfit = [105, 105, 179, 90];
      revenue = [130, 130, 209, 110];
    } else if (selectedProduct.value === 'subscription') {
      netProfit = [67, 71, 121, 55];
      revenue = [80, 85, 143, 65];
    } else { // license
      netProfit = [38, 34, 58, 35];
      revenue = [50, 45, 66, 45];
    }
  } else { // yearly
    labels = ['2022', '2023', '2024', '2025'];

    if (selectedProduct.value === 'all') {
      netProfit = [300, 350, 400, 335];
      revenue = [400, 450, 500, 430];
    } else if (selectedProduct.value === 'subscription') {
      netProfit = [180, 220, 260, 215];
      revenue = [240, 290, 320, 280];
    } else { // license
      netProfit = [120, 130, 140, 120];
      revenue = [160, 160, 180, 150];
    }
  }

  return {
    labels: labels,
    datasets: [
      {
        label: 'Net Profit',
        data: netProfit,
        backgroundColor: '#2ecc71', // Softer green
        borderColor: '#27ae60',     // Deeper green for contrast
        borderRadius: 4,
        borderWidth: 1
      },
      {
        label: 'Revenue',
        data: revenue,
        backgroundColor: '#3498db', // Softer blue
        borderColor: '#2980b9',     // Deeper blue
        borderRadius: 4,
        borderWidth: 1
      }
    ]
  };
});

const chartOptions = computed(() => {
  return {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      tooltip: {
        callbacks: {
          label: function (context) {
            let label = context.dataset.label || '';
            if (label) {
              label += ': ';
            }
            if (context.parsed.y !== null) {
              label += '$ ' + context.parsed.y + ' thousands';
            }
            return label;
          }
        }
      },
      legend: {
        position: 'bottom',
      }
    },
    scales: {
      y: {
        title: {
          display: true,
          text: '$ (thousands)'
        },
        beginAtZero: true
      }
    },
    animation: {
      duration: 500
    }
  };
});
</script>
