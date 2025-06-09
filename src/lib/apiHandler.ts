import axios from 'axios';
import {BACKEND_URL} from '@/static-default';

class apiHandler {
  async getDashboardStats() {
    try {
      const response = await axios.get(BACKEND_URL+'/dashboard');
      // Expected response format: { newUsers: 1523, revenue: "$12,340", orders: 345 }
      return response.data;
    } catch (error) {
      console.error('Error fetching dashboard stats:', error);
      throw error;
    }
  }
}

export default new apiHandler();
