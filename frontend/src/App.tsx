import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './pages/auth/LoginPage';
import RegisterPage from './pages/auth/RegisterPage';
import ProtectedRoute from './components/auth/ProtectedRoute';
import HomePage from './pages/HomePage';
import CVScoringPage from './pages/CVScoringPage';
import './index.css';

import Navbar from './components/layout/Navbar';

import MainLayout from './components/layout/MainLayout';

import { AuthProvider } from './context/AuthContext';

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
      <Routes>
        {/* Auth Routes: Không chứa Navbar chung */}
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />

        {/* Main Routes: Sử dụng MainLayout (có Navbar + Footer) */}
        <Route element={<MainLayout />}>
          <Route path="/" element={<HomePage />} />
          <Route path="/home" element={<Navigate to="/" replace />} />
          <Route path="/cv-score" element={<CVScoringPage />} />
          
          <Route element={<ProtectedRoute />}>
            {/* Thêm các trang yêu cầu đăng nhập ở đây */}
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
