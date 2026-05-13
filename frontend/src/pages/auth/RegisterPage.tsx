import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import AuthLayout from '../../components/auth/AuthLayout';
import { register } from '../../lib/auth';

const RegisterPage: React.FC = () => {
  const navigate = useNavigate();
  const [fullName, setFullName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setError('');
    setIsSubmitting(true);

    try {
      await register({ fullName, email, password });
      navigate('/dashboard', { replace: true });
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Dang ky that bai.');
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <AuthLayout title="Tao tai khoan" subtitle="Bat dau hanh trinh su nghiep voi AI">
      <form className="space-y-5" onSubmit={handleSubmit}>
        <div className="space-y-2">
          <label className="ml-1 text-[14px] font-medium text-apple-dark">Ho va ten</label>
          <input
            type="text"
            placeholder="Nguyen Van A"
            className="apple-input"
            value={fullName}
            onChange={(e) => setFullName(e.target.value)}
            required
          />
        </div>

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
          <label className="ml-1 text-[14px] font-medium text-apple-dark">Mat khau</label>
          <input
            type="password"
            placeholder="It nhat 8 ky tu"
            className="apple-input"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>

        <div className="pt-2">
          {error && <p className="mb-4 text-sm text-red-500">{error}</p>}
          <p className="mb-4 px-2 text-center text-[12px] text-apple-silver">
            Bang cach dang ky, ban dong y voi Dieu khoan dich vu va Chinh sach bao mat cua chung toi.
          </p>
          <button type="submit" className="apple-button" disabled={isSubmitting}>
            {isSubmitting ? 'Dang tao tai khoan...' : 'Tao tai khoan'}
          </button>
        </div>

        <div className="mt-6 text-center">
          <p className="text-[15px] text-apple-silver">
            Da co tai khoan?{' '}
            <Link to="/login" className="apple-link font-medium">
              Dang nhap
            </Link>
          </p>
        </div>
      </form>
    </AuthLayout>
  );
};

export default RegisterPage;
