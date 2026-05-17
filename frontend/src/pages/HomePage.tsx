import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const HomePage: React.FC = () => {
  const { user, isLoading } = useAuth();
  const navigate = useNavigate();

  const handleFeatureClick = (link: string) => {
    if (!user) {
      navigate('/login');
    } else {
      navigate(link);
    }
  };

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-apple-blue"></div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-mesh pt-32 pb-24 px-6 overflow-x-hidden">
      <div className="max-w-7xl mx-auto">
        {/* Header Section - Premium Typography */}
        <div className="max-w-5xl mb-20 animate-fade-in-up">
          <div className="inline-flex items-center space-x-2 px-3 py-1 rounded-full bg-apple-blue/10 border border-apple-blue/20 mb-6">
            <span className="relative flex h-2 w-2">
              <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-apple-blue opacity-75"></span>
              <span className="relative inline-flex rounded-full h-2 w-2 bg-apple-blue"></span>
            </span>
            <span className="text-[13px] font-bold text-apple-blue tracking-wider uppercase">AI Generation 3.0</span>
          </div>
          
          <h1 className="text-[42px] md:text-[68px] lg:text-[76px] font-bold tracking-tight text-apple-dark leading-[1.1] mb-8">
            Giải pháp thông minh <br className="hidden md:block" />
            <span className="text-gradient">cho sự nghiệp của bạn.</span>
          </h1>
          
          <p className="text-[19px] md:text-[22px] text-apple-silver leading-relaxed max-w-3xl font-medium">
            Sử dụng trí tuệ nhân tạo để phân tích và tối ưu hóa CV theo tiêu chuẩn toàn cầu. 
            <span className="text-apple-dark"> Nhanh chóng. Chính xác. Đẳng cấp.</span>
          </p>
        </div>

        {/* Features Bento Grid */}
        <div className="grid grid-cols-1 md:grid-cols-12 gap-8 h-auto">
          
          {/* Main Feature: CV Scoring - Large Premium Card */}
          <div 
            onClick={() => handleFeatureClick('/cv-score')}
            className="md:col-span-8 group relative overflow-hidden rounded-[40px] bg-white border border-gray-100 cursor-pointer p-12 flex flex-col justify-between transition-all duration-700 hover:shadow-[0_50px_100px_rgba(0,113,227,0.12)] hover:-translate-y-2 animate-fade-in-up [animation-delay:200ms]"
          >
            <div className="relative z-10">
              <div className="inline-flex items-center px-4 py-1.5 rounded-full bg-apple-blue text-white text-[12px] font-bold mb-8 shadow-lg shadow-apple-blue/30">
                PHỔ BIẾN NHẤT
              </div>
              <h3 className="text-[40px] font-bold text-apple-dark mb-6 tracking-tight">AI CV Scoring</h3>
              <p className="text-apple-silver text-[20px] max-w-md leading-relaxed">
                Đánh giá CV theo tiêu chuẩn ATS toàn cầu. Nhận phản hồi chi tiết và điểm số chuyên nghiệp ngay lập tức.
              </p>
            </div>
            
            {/* Improved Visual Element */}
            <div className="absolute right-[-5%] bottom-[-10%] w-[55%] h-[80%] hidden lg:block opacity-40 group-hover:opacity-100 transition-all duration-1000 group-hover:scale-105">
               <div className="relative w-full h-full">
                  <div className="absolute inset-0 bg-gradient-to-br from-apple-blue/20 to-accent-purple/20 blur-3xl rounded-full"></div>
                  <div className="relative z-10 w-full aspect-[4/3] bg-white rounded-3xl shadow-[0_30px_60px_rgba(0,0,0,0.1)] border border-gray-100 p-8 transform rotate-[-6deg] group-hover:rotate-[-2deg] transition-all duration-700">
                     <div className="space-y-6">
                        <div className="flex justify-between items-center">
                           <div className="h-3 w-24 bg-gray-100 rounded-full"></div>
                           <div className="h-8 w-8 rounded-full bg-green-500/10 flex items-center justify-center">
                              <div className="h-4 w-4 rounded-full bg-green-500"></div>
                           </div>
                        </div>
                        <div className="h-3 w-full bg-gray-50 rounded-full"></div>
                        <div className="h-3 w-5/6 bg-gray-50 rounded-full"></div>
                        <div className="pt-4 flex items-end justify-between">
                           <div className="space-y-2">
                              <div className="h-2 w-16 bg-blue-100 rounded-full"></div>
                              <div className="text-[42px] font-black text-apple-blue">88<span className="text-xl">/100</span></div>
                           </div>
                           <div className="h-24 w-2 bg-gray-50 rounded-full relative overflow-hidden">
                              <div className="absolute bottom-0 left-0 right-0 h-4/5 bg-apple-blue rounded-full"></div>
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
            </div>

            <div className="relative z-10 mt-12 flex items-center text-apple-blue font-bold text-lg group/btn cursor-pointer">
               <span className="mr-3">Trải nghiệm ngay</span>
               <div className="flex-shrink-0 w-10 h-10 rounded-full bg-apple-blue/10 flex items-center justify-center group-hover/btn:bg-apple-blue transition-all duration-300">
                  <svg 
                    className="w-5 h-5 transform group-hover/btn:translate-x-1 transition-transform duration-300 text-apple-blue group-hover/btn:text-white" 
                    fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth="2.5"
                  >
                    <path strokeLinecap="round" strokeLinejoin="round" d="M13.5 4.5L21 12m0 0l-7.5 7.5M21 12H3" />
                  </svg>
               </div>
            </div>
          </div>

          {/* Side Feature 1: Editor - Dark Professional Card */}
          <div 
            onClick={() => handleFeatureClick('/cv-editor')}
            className="md:col-span-4 group relative overflow-hidden rounded-[40px] bg-apple-dark cursor-pointer p-12 flex flex-col justify-between transition-all duration-700 hover:shadow-[0_50px_100px_rgba(0,0,0,0.3)] hover:-translate-y-2 animate-fade-in-up [animation-delay:400ms]"
          >
            <div className="relative z-10">
              <div className="w-14 h-14 rounded-2xl bg-white/10 flex items-center justify-center mb-8 border border-white/10">
                 <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"></path><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"></path></svg>
              </div>
              <h3 className="text-[28px] font-bold text-white mb-4 tracking-tight">Smart Editor</h3>
              <p className="text-gray-400 text-[18px] leading-relaxed">
                Tối ưu hóa nội dung CV bằng AI chuyên dụng cho từng ngành nghề.
              </p>
            </div>
            
            <div className="relative z-10 mt-8 flex items-center text-white font-semibold group/btn cursor-pointer">
               <span className="mr-2">Khám phá</span>
               <svg 
                  className="w-5 h-5 transform group-hover/btn:translate-x-1 transition-transform duration-300" 
                  fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth="2.5"
               >
                  <path strokeLinecap="round" strokeLinejoin="round" d="M13.5 4.5L21 12m0 0l-7.5 7.5M21 12H3" />
               </svg>
            </div>

            {/* Premium Glow Effect */}
            <div className="absolute -bottom-20 -right-20 w-64 h-64 bg-apple-blue/30 blur-[120px] rounded-full opacity-0 group-hover:opacity-100 transition-opacity duration-1000"></div>
          </div>

          {/* Bottom Feature: Mock Interview - Glass Style */}
          <div 
            onClick={() => handleFeatureClick('/mock-interview')}
            className="md:col-span-5 group relative overflow-hidden rounded-[40px] bg-white border border-gray-100 cursor-pointer p-12 flex flex-col justify-between transition-all duration-700 hover:shadow-[0_50px_100px_rgba(0,113,227,0.08)] hover:-translate-y-2 animate-fade-in-up [animation-delay:600ms]"
          >
            <div className="relative z-10">
               <div className="w-14 h-14 rounded-2xl bg-accent-indigo/10 flex items-center justify-center mb-8 border border-accent-indigo/10">
                  <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#6366f1" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2z"></path></svg>
               </div>
              <h3 className="text-[28px] font-bold text-apple-dark mb-4 tracking-tight">AI Mock Interview</h3>
              <p className="text-apple-silver text-[18px] leading-relaxed">
                Luyện tập phỏng vấn thực tế với phản hồi từ AI theo thời gian thực.
              </p>
            </div>
            <div className="relative z-10 mt-8 flex items-center text-accent-indigo font-bold group/btn cursor-pointer">
               <span className="mr-2">Bắt đầu luyện tập</span>
               <svg 
                  className="w-5 h-5 transform group-hover/btn:translate-x-1 transition-transform duration-300" 
                  fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth="2.5"
               >
                  <path strokeLinecap="round" strokeLinejoin="round" d="M13.5 4.5L21 12m0 0l-7.5 7.5M21 12H3" />
               </svg>
            </div>
          </div>

          {/* Dynamic Stats Card */}
          <div className="md:col-span-7 rounded-[40px] bg-gradient-to-br from-apple-dark to-[#2c2c2e] p-12 flex flex-col lg:flex-row items-center justify-between border border-white/5 shadow-2xl animate-fade-in-up [animation-delay:800ms]">
            <div className="grid grid-cols-2 gap-12 w-full lg:w-auto">
              <div className="space-y-1">
                <p className="text-[48px] font-black text-white tracking-tighter">10k<span className="text-apple-blue">+</span></p>
                <p className="text-gray-400 font-medium text-[16px] uppercase tracking-widest">CV phân tích</p>
              </div>
              <div className="space-y-1">
                <p className="text-[48px] font-black text-white tracking-tighter">98<span className="text-accent-indigo">%</span></p>
                <p className="text-gray-400 font-medium text-[16px] uppercase tracking-widest">Tin dùng</p>
              </div>
            </div>
            
            <div className="hidden lg:block w-px h-24 bg-white/10 mx-12"></div>
            
            <div className="mt-12 lg:mt-0 text-center lg:text-left">
               <div className="flex space-x-1 mb-4 justify-center lg:justify-start">
                  {[1,2,3,4,5].map(i => (
                     <svg key={i} width="16" height="16" viewBox="0 0 24 24" fill="#ffb800"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"></polygon></svg>
                  ))}
               </div>
               <p className="text-white/90 text-[18px] font-medium leading-relaxed italic max-w-xs">
                 "Công cụ không thể thiếu cho bất kỳ ai muốn bứt phá sự nghiệp."
               </p>
            </div>
          </div>
        </div>

        {/* Enhanced Footer CTA for Guests */}
        {!user && (
          <div className="mt-32 text-center animate-fade-in-up [animation-delay:1000ms]">
            <h2 className="text-[32px] font-bold text-apple-dark mb-4">Sẵn sàng để tỏa sáng?</h2>
            <p className="text-apple-silver text-[18px] mb-12 max-w-md mx-auto">Gia nhập cộng đồng 10,000+ người dùng thành công ngay hôm nay.</p>
            <button 
              onClick={() => navigate('/register')}
              className="group relative px-12 py-5 bg-apple-blue text-white rounded-full font-bold text-[20px] overflow-hidden transition-all duration-300 hover:scale-105 hover:shadow-[0_20px_40px_rgba(0,113,227,0.3)]"
            >
              <span className="relative z-10">Tạo tài khoản miễn phí</span>
              <div className="absolute inset-0 bg-gradient-to-r from-accent-indigo to-apple-blue opacity-0 group-hover:opacity-100 transition-opacity duration-300"></div>
            </button>
          </div>
        )}
      </div>
    </div>
  );
};

export default HomePage;
