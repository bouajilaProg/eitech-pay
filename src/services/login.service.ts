import axios from 'axios'

const API_URL = 'http://localhost:3000'; // Replace with your actual backend URL
const TOKEN_KEY = 'auth_token';

interface LoginCredentials {
  email: string;
  password: string;
}

interface LoginResponse {
  token: string;
  adminName: string;
}

interface SessionCheckResponse {
  adminName: string;
}

export class LoginService {
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

  async login(credentials: LoginCredentials): Promise<LoginResponse> {
    const response = await this.api.post<LoginResponse>('/login', credentials);

    const { token, adminName } = response.data;
    localStorage.setItem(this.tokenKey, token);
    return { token, adminName };
  }

  async checkSession(): Promise<SessionCheckResponse> {
    const token = this.getToken();

    if (!token) throw new Error('No token found');

    const response = await this.api.get<SessionCheckResponse>('/check-session', {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });

    return response.data;
  }

  logoff(): void {
    localStorage.removeItem(this.tokenKey);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
}

export const loginService = new LoginService();
