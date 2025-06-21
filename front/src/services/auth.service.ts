import { BaseService } from './base.service';

export type LoginInput = {
  email: string;
  password: string;
};

export type RegisterInput = {
  email: string;
  password: string;
};

export type AuthResponse = {
  token: string;
};

class AuthService extends BaseService {
  constructor() {
    super();
  }

  async login(data: LoginInput): Promise<AuthResponse> {
    const res = await this.api.post('/auth/login', data);
    this.saveToken(res.data.token);
    return res.data;
  }

  async register(data: RegisterInput): Promise<AuthResponse> {
    const res = await this.api.post('/auth/register', data);
    this.saveToken(res.data.token);
    return res.data;
  }

  logout() {
    localStorage.removeItem("token");
  }

//   getCurrentUser(): AuthResponse['user'] | null {
//     const user = localStorage.getItem('auth_user');
//     return user ? JSON.parse(user) : null;
//   }

  private saveToken(token: string) {
    localStorage.setItem("token", token);
    // Optionally save user info here if your backend returns it
  }
}

export const authService = new AuthService();
