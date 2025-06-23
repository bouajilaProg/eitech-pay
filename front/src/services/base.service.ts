import axios, {
    type AxiosInstance,
    type InternalAxiosRequestConfig
  } from 'axios';
  
  const API_URL = 'http://localhost:5080/api';
  const TOKEN_KEY = 'auth_token';
  
  function getAuthToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }
  
  export abstract class BaseService {
    protected readonly api: AxiosInstance;
  
    constructor() {
      this.api = axios.create({
        baseURL: API_URL,
        headers: {
          'Content-Type': 'application/json',
        }
      });
  
      this.api.interceptors.request.use((config: InternalAxiosRequestConfig) => {
        const token = getAuthToken();
        if (token) {
          // Set the header directly using AxiosHeaders
          config.headers.set?.('Authorization', `Bearer ${token}`);
        }
        return config;
      });
    }
  }
  