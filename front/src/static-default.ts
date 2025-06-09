// Static default data for the project

// Backend URL from environment variable
export const BACKEND_URL = "http://localhost:3000";

// Chart data for RevenueChart components
export const defaultChartData = {
  labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
  datasets: [
    {
      label: 'Revenue',
      data: [0, 0, 0, 0, 20 * 10 + 10 * 20, 280 * 10 + 140 * 20],
      backgroundColor: '#30d5c8'
    }
  ]
};

export const defaultChartOptions = {
  responsive: true,
  plugins: {
    legend: {
      position: 'bottom'
    },
    title: {
      display: false
    }
  },
  scales: {
    y: {
      beginAtZero: true
    }
  }
};

// Default tier templates for AddTierModal
export const defaultTierTemplates = [
  { id: 'tier_1', name: 'Basic', price: 10, duration: '1 month' },
  { id: 'tier_2', name: 'Standard', price: 25, duration: '3 months' },
  { id: 'tier_3', name: 'Premium', price: 50, duration: '6 months' }
]; 
