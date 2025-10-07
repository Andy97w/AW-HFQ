const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://localhost:7299';

export const API_ENDPOINTS = {
  USERS_SUMMARY: `${API_BASE_URL}/api/users/summary`
};

export { API_BASE_URL };