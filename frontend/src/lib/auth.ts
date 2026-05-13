import { apiRequest } from './api';

const ACCESS_TOKEN_KEY = 'cvscore_access_token';

export type AuthResponse = {
  userId: string;
  fullName: string;
  email: string;
  avatarUrl?: string | null;
  accessToken: string;
  expiresAtUtc: string;
};

export type CurrentUser = {
  userId: string;
  fullName: string;
  email: string;
  avatarUrl?: string | null;
};

export type LoginPayload = {
  email: string;
  password: string;
};

export type RegisterPayload = {
  fullName: string;
  email: string;
  password: string;
};

export function getAccessToken(): string | null {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

export function setAccessToken(token: string): void {
  localStorage.setItem(ACCESS_TOKEN_KEY, token);
}

export function clearAccessToken(): void {
  localStorage.removeItem(ACCESS_TOKEN_KEY);
}

export async function login(payload: LoginPayload): Promise<AuthResponse> {
  const result = await apiRequest<AuthResponse>('/api/auth/login', {
    method: 'POST',
    body: JSON.stringify(payload),
  });

  setAccessToken(result.accessToken);
  return result;
}

export async function register(payload: RegisterPayload): Promise<AuthResponse> {
  const result = await apiRequest<AuthResponse>('/api/auth/register', {
    method: 'POST',
    body: JSON.stringify(payload),
  });

  setAccessToken(result.accessToken);
  return result;
}

export async function getCurrentUser(): Promise<CurrentUser> {
  return apiRequest<CurrentUser>('/api/auth/me', {}, getAccessToken());
}
