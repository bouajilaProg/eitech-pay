import axios from 'axios';

const API_URL = 'http://localhost:5225/api'; // Replace with your actual backend URL
const TOKEN_KEY = 'auth_token';

type Product = {
  id: number;
  name: string;
  description: string;
  type: string;
};

type CreateProductInput = Omit<Product, 'id'>;
type UpdateProductInput = Partial<Omit<Product, 'id'>>;

export class ProductService {
  private readonly tokenKey = TOKEN_KEY;
  private readonly api;

  constructor() {
    this.api = axios.create({
      baseURL: API_URL,
      headers: {
        'Content-Type': 'application/json'
      }
    });
  }

  private getAuthHeaders() {
    const token = this.getToken();
    if (!token) throw new Error('No token found');
    return {
      Authorization: `Bearer ${token}`
    };
  }

  async getAll(): Promise<Product[]> {
    const response = await this.api.get<Product[]>('/products');
    return response.data;
  }

  async getById(id: string): Promise<Product> {
    const response = await this.api.get<Product>(`/products/${id}`);
    return response.data;
  }

  async create(data: CreateProductInput): Promise<Product> {
    const response = await this.api.post<Product>('/products', data);
    return response.data;
  }

  async update(id: string, data: UpdateProductInput): Promise<Product> {
    const response = await this.api.put<Product>(`/products/${id}`, data);
    return response.data;
  }

  async delete(id: string): Promise<void> {
    await this.api.delete(`/products/${id}`);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
}

export const productService = new ProductService();
