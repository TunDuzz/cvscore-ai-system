import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';

const Navbar: React.FC = () => {
  const { user, logout } = useAuth();
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [isScrolled, setIsScrolled] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      setIsScrolled(window.scrollY > 10);
    };

    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  return (
    <nav 
      className={`fixed top-0 left-0 right-0 z-50 transition-all duration-300 ${
        isScrolled ? 'bg-white/80 backdrop-blur-md shadow-sm py-3' : 'bg-transparent py-5'
      }`}
    >
      <div className="max-w-7xl mx-auto px-6 flex items-center justify-between">
        {/* Logo */}
        <Link to="/" className="flex items-center space-x-2">
          <div className="w-8 h-8 bg-apple-blue rounded-lg flex items-center justify-center">
            <span className="text-white font-bold text-lg">C</span>
          </div>
          <span className="text-[21px] font-semibold tracking-tight text-apple-dark">CVScore AI</span>
        </Link>

        {/* Desktop Navigation */}
        <div className="hidden md:flex items-center space-x-8">
          <Link to="/" className="text-[15px] font-medium text-apple-silver hover:text-apple-dark transition-colors">Tính năng</Link>
          <a href="#" className="text-[15px] font-medium text-apple-silver hover:text-apple-dark transition-colors">Bảng giá</a>
          <a href="#" className="text-[15px] font-medium text-apple-silver hover:text-apple-dark transition-colors">Về chúng tôi</a>
        </div>

        {/* Auth / Profile */}
        <div className="hidden md:flex items-center space-x-4">
          {user ? (
            <div className="flex items-center space-x-4">
              <div className="text-right">
                <p className="text-[14px] font-semibold text-apple-dark leading-tight">{user.fullName}</p>
                <button 
                  onClick={logout}
                  className="text-[12px] text-apple-silver hover:text-red-500 transition-colors"
                >
                  Đăng xuất
                </button>
              </div>
              <div className="w-10 h-10 rounded-full bg-apple-gray border border-gray-100 flex items-center justify-center overflow-hidden">
                {user.avatarUrl ? (
                  <img src={user.avatarUrl} alt={user.fullName} className="w-full h-full object-cover" />
                ) : (
                  <span className="text-apple-silver font-medium">{user.fullName.charAt(0)}</span>
                )}
              </div>
            </div>
          ) : (
            <>
              <Link to="/login" className="text-[15px] font-medium text-apple-dark hover:text-apple-blue transition-colors px-4 py-2">
                Đăng nhập
              </Link>
              <Link to="/register" className="apple-button !w-auto px-6 !py-2 !rounded-full text-[15px]">
                Bắt đầu ngay
              </Link>
            </>
          )}
        </div>

        {/* Mobile Toggle */}
        <button 
          className="md:hidden p-2 text-apple-dark"
          onClick={() => setIsMenuOpen(!isMenuOpen)}
        >
          {isMenuOpen ? (
            <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
            </svg>
          ) : (
            <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
            </svg>
          )}
        </button>
      </div>

      {/* Mobile Menu */}
      {isMenuOpen && (
        <div className="md:hidden absolute top-full left-0 right-0 bg-white border-t border-gray-100 p-6 space-y-4 shadow-xl animate-fade-in-up">
          <Link to="/" className="block text-lg font-medium text-apple-dark">Tính năng</Link>
          <a href="#" className="block text-lg font-medium text-apple-dark">Bảng giá</a>
          <a href="#" className="block text-lg font-medium text-apple-dark">Về chúng tôi</a>
          <hr className="border-gray-100" />
          {user ? (
            <div className="flex items-center justify-between">
              <span className="font-medium text-apple-dark">{user.fullName}</span>
              <button onClick={logout} className="text-red-500 font-medium">Đăng xuất</button>
            </div>
          ) : (
            <div className="grid grid-cols-2 gap-4">
              <Link to="/login" className="apple-button bg-apple-gray !text-apple-dark text-center">Đăng nhập</Link>
              <Link to="/register" className="apple-button text-center">Bắt đầu</Link>
            </div>
          )}
        </div>
      )}
    </nav>
  );
};

export default Navbar;
