<template>
  <section class="my-8">
    <h2 class="text-2xl font-bold mb-4">Revenue Chart</h2>
    <div class="bg-white p-4 rounded-lg shadow-md">
      <Bar :data="chartData" :options="chartOptions" />
    </div>
  </section>
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
import { defaultChartData, defaultChartOptions } from '../../static-default.ts';

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

const chartData = defaultChartData;
const chartOptions = defaultChartOptions;
</script>
