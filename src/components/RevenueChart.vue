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
import { defaultChartData, defaultChartOptions } from '../static-default.ts';

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
      revenue = [0, 0, 0, 400, 5600, 0,0];
    } else if (selectedProduct.value === 'subscription') {
      revenue = [0, 0, 0, 0, 0, 0, 0];
    } else { // license
      revenue = [0, 0, 0, 0, 0, 0, 0];
    }
  } else if (selectedPeriod.value === 'quarterly') {
    labels = ['Q1', 'Q2', 'Q3', 'Q4'];

    if (selectedProduct.value === 'all') {
      revenue = [130, 130, 209, 110];
    } else if (selectedProduct.value === 'subscription') {
      revenue = [80, 85, 143, 65];
    } else { // license
      revenue = [50, 45, 66, 45];
    }
  } else { // yearly
    labels = ['2022', '2023', '2024', '2025'];

    if (selectedProduct.value === 'all') {
      revenue = [400, 450, 500, 430];
    } else if (selectedProduct.value === 'subscription') {
      revenue = [240, 290, 320, 280];
    } else { // license
      revenue = [160, 160, 180, 150];
    }
  }

  return {
    labels: labels,
    datasets: [
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
