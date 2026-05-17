import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import AuthLayout from '../../components/auth/AuthLayout';
import { login } from '../../lib/auth';

const LoginPage: React.FC = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setError('');
    setIsSubmitting(true);

    try {
      await login({ email, password });
      navigate('/', { replace: true });
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Đăng nhập thất bại.');
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <AuthLayout title="Đăng nhập" subtitle="Sử dụng tài khoản CVScore của bạn">
      <form className="space-y-6" onSubmit={handleSubmit}>
        <div className="space-y-2">
          <label className="ml-1 text-[14px] font-medium text-apple-dark">Email</label>
          <input
            type="email"
            placeholder="name@example.com"
            className="apple-input"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>

        <div className="space-y-2">
          <div className="flex items-center justify-between px-1">
            <label className="text-[14px] font-medium text-apple-dark">Mật khẩu</label>
            <span className="apple-link text-[13px] opacity-70">Quên mật khẩu?</span>
          </div>
          <input
            type="password"
            placeholder="••••••••"
            className="apple-input"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>

        {error && <p className="text-sm text-red-500">{error}</p>}

        <button type="submit" className="apple-button mt-4" disabled={isSubmitting}>
          {isSubmitting ? 'Đang đăng nhập...' : 'Tiếp tục'}
        </button>

        <div className="mt-6 text-center">
          <p className="text-[15px] text-apple-silver">
            Chưa có tài khoản?{' '}
            <Link to="/register" className="apple-link font-medium">
              Đăng ký ngay
            </Link>
          </p>
        </div>
      </form>
    </AuthLayout>
  );
};

export default LoginPage;
