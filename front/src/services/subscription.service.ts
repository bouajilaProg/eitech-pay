import { BaseService } from './base.service';

export type Subscription = {
  id: number;
  name: string;
  description: string;
};

export type SubscriptionTier = {
  id: number;
  subsciptionId: number;
  tierName: string;
  duration: number;
  gracePeriod: number;
  price: number;
};

export type CreateSubscriptionInput = Omit<Subscription, 'id'>;
export type UpdateSubscriptionInput = Partial<Omit<Subscription, 'id'>>;

export type CreateTierInput = Omit<SubscriptionTier, 'tierId'>;
export type UpdateTierInput = Partial<Omit<SubscriptionTier, 'tierId'>>;

class SubscriptionService extends BaseService {
  constructor() {
    super();
  }

  // Subscription endpoints
  async getAllSubscriptions(): Promise<Subscription[]> {
    const res = await this.api.get('/subscription');
    return res.data;
  }

  async getSubscriptionById(subId: number): Promise<Subscription> {
    const res = await this.api.get(`/subscription/${subId}`);
    return res.data;
  }

  async createSubscription(data: CreateSubscriptionInput): Promise<Subscription> {
    const res = await this.api.post('/subscription', data);
    return res.data;
  }

  async updateSubscription(subId: number, data: UpdateSubscriptionInput): Promise<Subscription> {
    const res = await this.api.put(`/subscription/${subId}`, data);
    return res.data;
  }

  async deleteSubscription(subId: number): Promise<void> {
    await this.api.delete(`/subscription/${subId}`);
  }

  // Subscription Tier endpoints
  async getAllTiers(): Promise<SubscriptionTier[]> {
    const res = await this.api.get('/subscription/tiers');
    return res.data;
  }

  async getTierById(tierId: number): Promise<SubscriptionTier> {
    const res = await this.api.get(`/subscription/tiers/${tierId}`);
    return res.data;
  }

  async createTier(data: CreateTierInput): Promise<SubscriptionTier> {
    const res = await this.api.post('/subscription/tiers', data);
    return res.data;
  }

  async updateTier(tierId: number, data: UpdateTierInput): Promise<SubscriptionTier> {
    const res = await this.api.put(`/subscription/tiers/${tierId}`, data);
    return res.data;
  }

  async deleteTier(tierId: number): Promise<void> {
    await this.api.delete(`/subscription/tiers/${tierId}`);
  }
}

export const subscriptionService = new SubscriptionService();
