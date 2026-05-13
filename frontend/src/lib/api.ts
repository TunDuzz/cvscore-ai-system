const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5185';

type ApiError = {
  code?: string;
  message?: string;
};

type ApiEnvelope<T> = {
  success: boolean;
  data?: T;
  error?: ApiError;
};

export async function apiRequest<T>(
  path: string,
  options: RequestInit = {},
  token?: string | null,
): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...(options.headers ?? {}),
    },
  });

  const isJson = response.headers.get('content-type')?.includes('application/json');
  const payload = isJson ? ((await response.json()) as ApiEnvelope<T>) : null;

  if (!response.ok || !payload?.success) {
    throw new Error(payload?.error?.message ?? 'Request failed.');
  }

  if (payload.data === undefined) {
    throw new Error('Response payload is missing data.');
  }

  return payload.data;
}

export { API_BASE_URL };
