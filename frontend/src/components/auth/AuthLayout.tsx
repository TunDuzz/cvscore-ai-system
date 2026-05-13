import React from 'react';

interface AuthLayoutProps {
  children: React.ReactNode;
  title: string;
  subtitle?: string;
}

const AuthLayout: React.FC<AuthLayoutProps> = ({ children, title, subtitle }) => {
  return (
    <div className="min-h-screen w-full flex flex-col items-center justify-center bg-[#fafafa] px-4">
      <div className="w-full max-w-[400px] animate-fade-in-up">
        <div className="text-center mb-8">
          <h1 className="text-[32px] font-semibold tracking-tight text-apple-dark mb-2">
            {title}
          </h1>
          {subtitle && (
            <p className="text-apple-silver text-[17px]">
              {subtitle}
            </p>
          )}
        </div>
        
        <div className="auth-card">
          {children}
        </div>
        
        <div className="mt-8 text-center">
          <p className="text-[14px] text-apple-silver">
            © 2026 CVScore AI. Built for professionals.
          </p>
        </div>
      </div>
    </div>
  );
};

export default AuthLayout;
