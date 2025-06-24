import { BaseService } from './base.service';

export type Licence = {
  id: string;
  name: string;
  description: string;
  maxDevices: number;
  duration: number;
  gracePeriod: number;
  price: number;
};

export type LicenceOption = {
  id: number;
  licenceId: number;
  optionName: string;
  description: string;
  price: number;
};

type CreateOrUpdateLicenceInput = Omit<Licence, 'licenceId'>;
type CreateOrUpdateOptionInput = Omit<LicenceOption, 'optionId'>;

class LicenceService extends BaseService {
  // --- Licence CRUD ---
  async getAllLicences(): Promise<Licence[]> {
    const res = await this.api.get<Licence[]>('/licence');
    return res.data;
  }

  async getLicenceById(id: number): Promise<Licence> {
    const res = await this.api.get<Licence>(`/licence/${id}`);
    return res.data;
  }

  async createLicence(data: CreateOrUpdateLicenceInput): Promise<Licence> {
    const res = await this.api.post<Licence>('/licence', data);
    return res.data;
  }

  async updateLicence(id: number, data: CreateOrUpdateLicenceInput): Promise<Licence> {
    const res = await this.api.put<Licence>(`/licence/${id}`, data);
    return res.data;
  }

  async deleteLicence(id: number): Promise<void> {
    await this.api.delete(`/licence/${id}`);
  }

  // --- Licence Options ---
  async getAllOptions(): Promise<LicenceOption[]> {
    const res = await this.api.get<LicenceOption[]>('/licence/options');
    return res.data;
  }

  async createOption(data: CreateOrUpdateOptionInput): Promise<LicenceOption> {
    const res = await this.api.post<LicenceOption>('/licence/options', data);
    return res.data;
  }

  async getOptionById(id: number): Promise<LicenceOption> {
    const res = await this.api.get<LicenceOption>(`/licence/options/${id}`);
    return res.data;
  }

  async updateOption(id: number, data: CreateOrUpdateOptionInput): Promise<LicenceOption> {
    const res = await this.api.put<LicenceOption>(`/licence/options/${id}`, data);
    return res.data;
  }

  async deleteOption(id: number): Promise<void> {
    await this.api.delete(`/licence/options/${id}`);
  }

  async getOptionsByLicenceId(licenceId: number): Promise<LicenceOption[]> {
    const res = await this.api.get<LicenceOption[]>(`/licence/${licenceId}/options`);
    return res.data;
  }
}

export const licenceService = new LicenceService();
