<template>
  <div class="card">
    <div class="card-header">
      <div class="flex w-full justify-between">
        <h3 class="card-title">Revenue</h3>
        <div class="flex">
          <div class="me-3">
            <select v-model="selectedPeriod" class="form-select form-select-md">
              <option value="monthly">Monthly</option>
              <option value="quarterly">Quarterly</option>
              <option value="yearly">Yearly</option>
            </select>
          </div>
        </div>
      </div>
    </div>
    <div class="bg-white p-4 rounded-lg shadow-md">
      <Bar :data="chartData" :options="chartOptions" />
    </div>
  </div>
</template>

<script setup>
import { Bar } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale
} from 'chart.js'
import { ref, computed } from 'vue'
import { defaultChartOptions } from '../../static-default.ts'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

const selectedPeriod = ref('monthly')

const chartData = computed(() => {
  let labels = []
  let data = []

  if (selectedPeriod.value === 'monthly') {
    labels = ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug']
    data = [0, 0, 0, 400, 5600, 0, 0]
  } else if (selectedPeriod.value === 'quarterly') {
    labels = ['Q1', 'Q2', 'Q3', 'Q4']
    data = [130, 130, 209, 110]
  } else if (selectedPeriod.value === 'yearly') {
    labels = ['2022', '2023', '2024', '2025']
    data = [400, 450, 500, 430]
  }

  return {
    labels,
    datasets: [
      {
        label: 'Revenue',
        data,
        backgroundColor: '#3498db',
        borderColor: '#2980b9',
        borderRadius: 4,
        borderWidth: 1
      }
    ]
  }
})

const chartOptions = defaultChartOptions
</script>
