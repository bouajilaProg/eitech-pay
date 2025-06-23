import { BaseService } from './base.service';

export type LoginInput = {
  email: string;
  password: string;
  captcha?: string; 
};

export type AuthResponse = {
  token: string;
};

class AuthService extends BaseService {
  async login(data: LoginInput): Promise<AuthResponse> {
    const res = await this.api.post<AuthResponse>('/auth/login', data);
    return res.data;
  }

  logout(): void {
    localStorage.removeItem('auth_token');
  }
}

export const authService = new AuthService();
