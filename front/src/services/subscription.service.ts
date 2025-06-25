import { BaseService } from './base.service';

// Backend uses string IDs — update types accordingly
export type Subscription = {
  id: string;
  name: string;
  description: string;
};

export type SubscriptionTier = {
  id: string;
  subscriptionId: string;
  tierName: string;
  duration: number;
  gracePeriod: number;
  price: number;
};

export type CreateSubscriptionInput = Omit<Subscription, 'id'>;
export type UpdateSubscriptionInput = Partial<Omit<Subscription, 'id'>>;

export type CreateTierInput = Omit<SubscriptionTier, 'id'>;
export type UpdateTierInput = Partial<Omit<SubscriptionTier, 'id'>>;

class SubscriptionService extends BaseService {
  // --- Subscription CRUD ---

  async getAllSubscriptions(): Promise<Subscription[]> {
    const res = await this.api.get('/subscription');
    return res.data;
  }

  async getSubscriptionById(subId: string): Promise<Subscription> {
    const res = await this.api.get(`/subscription/${subId}`);
    return res.data;
  }

  async createSubscription(data: CreateSubscriptionInput): Promise<string> {
    const res = await this.api.post<string>('/subscription', data);
    return res.data;
  }

  async updateSubscription(subId: string, data: UpdateSubscriptionInput): Promise<void> {
    await this.api.put(`/subscription/${subId}`, data);
  }

  async deleteSubscription(subId: string): Promise<void> {
    await this.api.delete(`/subscription/${subId}`);
  }

  // --- Tier CRUD ---

  async getAllTiers(): Promise<SubscriptionTier[]> {
    const res = await this.api.get('/subscription/tiers');
    return res.data;
  }

  async getTierById(tierId: string): Promise<SubscriptionTier> {
    const res = await this.api.get(`/subscription/tiers/${tierId}`);
    return res.data;
  }

  async getTiersBySubscriptionId(subId: string): Promise<SubscriptionTier[]> {
    const res = await this.api.get(`/subscription/${subId}/tiers`);
    return res.data;
  }

  async createTier(data: CreateTierInput): Promise<string> {
    const res = await this.api.post<string>('/subscription/tiers', data);
    return res.data;
  }

  async updateTier(tierId: string, data: UpdateTierInput): Promise<void> {
    await this.api.put(`/subscription/tiers/${tierId}`, data);
  }

  async deleteTier(tierId: string): Promise<void> {
    await this.api.delete(`/subscription/tiers/${tierId}`);
  }
}

export const subscriptionService = new SubscriptionService();
