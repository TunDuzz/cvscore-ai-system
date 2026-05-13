import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { getAccessToken } from '../../lib/auth';

const ProtectedRoute: React.FC = () => {
  const token = getAccessToken();

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  return <Outlet />;
};

export default ProtectedRoute;
