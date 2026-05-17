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
      navigate('/', { replace: true });
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Đăng ký thất bại.');
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <AuthLayout title="Tạo tài khoản" subtitle="Bắt đầu hành trình sự nghiệp với AI">
      <form className="space-y-5" onSubmit={handleSubmit}>
        <div className="space-y-2">
          <label className="ml-1 text-[14px] font-medium text-apple-dark">Họ và tên</label>
          <input
            type="text"
            placeholder="Nguyễn Văn A"
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
          <label className="ml-1 text-[14px] font-medium text-apple-dark">Mật khẩu</label>
          <input
            type="password"
            placeholder="Ít nhất 8 ký tự"
            className="apple-input"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>

        <div className="pt-2">
          {error && <p className="mb-4 text-sm text-red-500">{error}</p>}
          <p className="mb-4 px-2 text-center text-[12px] text-apple-silver">
            Bằng cách đăng ký, bạn đồng ý với Điều khoản dịch vụ và Chính sách bảo mật của chúng tôi.
          </p>
          <button type="submit" className="apple-button" disabled={isSubmitting}>
            {isSubmitting ? 'Đang tạo tài khoản...' : 'Tạo tài khoản'}
          </button>
        </div>

        <div className="mt-6 text-center">
          <p className="text-[15px] text-apple-silver">
            Đã có tài khoản?{' '}
            <Link to="/login" className="apple-link font-medium">
              Đăng nhập
            </Link>
          </p>
        </div>
      </form>
    </AuthLayout>
  );
};

export default RegisterPage;
