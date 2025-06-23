import { BaseService } from './base.service';

const TOKEN_KEY = 'auth_token';

export type LoginInput = {
  username: string;
  password: string;
  captcha?: string;
};

export type AuthResponse = {
  token: string;
  adminName?: string; // optional if needed
};

export type SessionCheckResponse = {
  adminName: string;
};

class AuthService extends BaseService {
  private readonly tokenKey = TOKEN_KEY;

  async login(data: LoginInput): Promise<AuthResponse> {
    const res = await this.api.post<AuthResponse>('/admin/login', data);
    const { token } = res.data;

    localStorage.setItem(this.tokenKey, token);

    return res.data;
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  async checkSession(): Promise<SessionCheckResponse> {
    const token = this.getToken();
    if (!token) throw new Error('No token found');

    const res = await this.api.get<SessionCheckResponse>('/auth/check-session', {
      headers: { Authorization: `Bearer ${token}` },
    });

    return res.data;
  }
}

export const authService = new AuthService();
