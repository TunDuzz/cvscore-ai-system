import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { clearAccessToken, getCurrentUser, type CurrentUser } from '../lib/auth';

const DashboardPage: React.FC = () => {
  const navigate = useNavigate();
  const [user, setUser] = useState<CurrentUser | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    let cancelled = false;

    void (async () => {
      try {
        const currentUser = await getCurrentUser();

        if (!cancelled) {
          setUser(currentUser);
        }
      } catch (err) {
        if (!cancelled) {
          clearAccessToken();
          setError(err instanceof Error ? err.message : 'Khong the tai thong tin nguoi dung.');
          navigate('/login', { replace: true });
        }
      } finally {
        if (!cancelled) {
          setLoading(false);
        }
      }
    })();

    return () => {
      cancelled = true;
    };
  }, [navigate]);

  const handleLogout = () => {
    clearAccessToken();
    navigate('/login', { replace: true });
  };

  return (
    <div className="min-h-screen bg-[#f5f5f7] px-6 py-10">
      <div className="mx-auto max-w-4xl rounded-[28px] bg-white p-8 shadow-[0_24px_60px_rgba(0,0,0,0.08)]">
        <div className="flex items-start justify-between gap-4">
          <div>
            <p className="text-sm uppercase tracking-[0.18em] text-apple-silver">CVScore AI</p>
            <h1 className="mt-2 text-4xl font-semibold tracking-tight text-apple-dark">
              {loading ? 'Dang tai...' : `Xin chao, ${user?.fullName ?? 'ban'}`}
            </h1>
            <p className="mt-3 max-w-2xl text-[17px] leading-7 text-apple-silver">
              Frontend auth da noi voi backend. Tu day ban co the tiep tuc dung dashboard va flow mock interview.
            </p>
          </div>

          <button type="button" className="apple-button max-w-[160px]" onClick={handleLogout}>
            Dang xuat
          </button>
        </div>

        <div className="mt-10 grid gap-4 md:grid-cols-2">
          <div className="rounded-[20px] border border-gray-100 bg-[#fafafa] p-5">
            <p className="text-sm text-apple-silver">Email</p>
            <p className="mt-2 text-lg font-medium text-apple-dark">{user?.email ?? '-'}</p>
          </div>

          <div className="rounded-[20px] border border-gray-100 bg-[#fafafa] p-5">
            <p className="text-sm text-apple-silver">User ID</p>
            <p className="mt-2 break-all text-sm font-medium text-apple-dark">{user?.userId ?? '-'}</p>
          </div>
        </div>

        {error && <p className="mt-6 text-sm text-red-500">{error}</p>}
      </div>
    </div>
  );
};

export default DashboardPage;
