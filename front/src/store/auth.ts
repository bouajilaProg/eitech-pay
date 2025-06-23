import { defineStore } from 'pinia';
import { ref, computed } from 'vue';
import { authService, type LoginInput } from '../services/auth.service';

export const useAuthStore = defineStore('auth', () => {
  const token = ref<string | null>(localStorage.getItem('auth_token'));
  const loginAttempts = ref<number>(0);
  const isCaptchaRequired = computed(() => loginAttempts.value >= 3);
  const isAuthenticated = computed(() => !!token.value);

  async function login(payload: LoginInput): Promise<boolean> {
    try {
      const response = await authService.login(payload);
      token.value = response.token;
      localStorage.setItem('auth_token', response.token);
      loginAttempts.value = 0;
      return true;
    } catch (error) {
      loginAttempts.value++;
      return false;
    }
  }

  function logout() {
    token.value = null;
    localStorage.removeItem('auth_token');
    authService.logout();
  }

  function resetLoginAttempts() {
    loginAttempts.value = 0;
  }

  return {
    token,
    login,
    logout,
    loginAttempts,
    isCaptchaRequired,
    isAuthenticated,
    resetLoginAttempts,
  };
});
