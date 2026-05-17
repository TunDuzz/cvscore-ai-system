import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const CVScoringPage: React.FC = () => {
  const [file, setFile] = useState<File | null>(null);
  const [jd, setJd] = useState('');
  const [isAnalyzing, setIsAnalyzing] = useState(false);
  const [showResults, setShowResults] = useState(false);
  const navigate = useNavigate();

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files[0]) {
      setFile(e.target.files[0]);
    }
  };

  const handleAnalyze = async () => {
    if (!file) return;
    setIsAnalyzing(true);
    
    // Giả lập quá trình phân tích của AI
    setTimeout(() => {
      setIsAnalyzing(false);
      setShowResults(true);
      window.scrollTo({ top: 0, behavior: 'smooth' });
    }, 3000);
  };

  if (showResults) {
    return (
      <div className="min-h-screen bg-mesh pt-32 pb-24 px-6">
        <div className="max-w-6xl mx-auto space-y-10 animate-fade-in-up">
          {/* Results Header */}
          <div className="flex flex-col md:flex-row md:items-center justify-between gap-6">
            <div>
              <button 
                onClick={() => setShowResults(false)}
                className="text-apple-blue font-medium flex items-center space-x-2 mb-4 hover:underline"
              >
                <span>&larr;</span> <span>Quay lại tải lên</span>
              </button>
              <h1 className="text-[42px] font-bold text-apple-dark tracking-tight">Kết quả phân tích</h1>
              <p className="text-apple-silver text-lg italic">Dành cho vị trí: {jd ? "Phù hợp với JD đã cung cấp" : "Đánh giá chung"}</p>
            </div>
            <div className="flex space-x-4">
              <button className="px-6 py-3 bg-white border border-gray-200 rounded-2xl font-semibold text-apple-dark hover:bg-gray-50 transition-all">
                Tải báo cáo (PDF)
              </button>
              <button 
                onClick={() => navigate('/cv-editor')}
                className="px-6 py-3 bg-apple-blue text-white rounded-2xl font-semibold hover:shadow-lg hover:shadow-apple-blue/20 transition-all"
              >
                Sửa CV ngay
              </button>
            </div>
          </div>

          <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
            {/* Score Overview */}
            <div className="lg:col-span-1 glass-card rounded-[40px] p-10 flex flex-col items-center justify-center text-center space-y-6">
              <div className="relative w-48 h-48 flex items-center justify-center">
                <svg className="w-full h-full transform -rotate-90">
                  <circle cx="96" cy="96" r="88" stroke="currentColor" strokeWidth="12" fill="transparent" className="text-gray-100" />
                  <circle 
                    cx="96" cy="96" r="88" stroke="currentColor" strokeWidth="12" fill="transparent" 
                    strokeDasharray={552.92}
                    strokeDashoffset={552.92 * (1 - 0.85)}
                    className="text-apple-blue transition-all duration-1000 ease-out" 
                  />
                </svg>
                <div className="absolute inset-0 flex flex-col items-center justify-center">
                  <span className="text-6xl font-black text-apple-dark tracking-tighter">85</span>
                  <span className="text-apple-silver font-bold uppercase tracking-widest text-xs">Điểm hệ 100</span>
                </div>
              </div>
              <div>
                <h2 className="text-2xl font-bold text-apple-dark">Khá Ấn Tượng!</h2>
                <p className="text-apple-silver mt-2 leading-relaxed">CV của bạn vượt xa 85% ứng viên khác trong cùng lĩnh vực.</p>
              </div>
            </div>

            {/* Breakdown */}
            <div className="lg:col-span-2 glass-card rounded-[40px] p-10 grid grid-cols-1 md:grid-cols-2 gap-10">
              {[
                { label: 'Từ khóa chuyên ngành', score: 92, color: 'text-green-500' },
                { label: 'Định dạng & Bố cục', score: 78, color: 'text-blue-500' },
                { label: 'Tác động & Con số', score: 88, color: 'text-indigo-500' },
                { label: 'Kinh nghiệm phù hợp', score: 82, color: 'text-purple-500' }
              ].map((item, i) => (
                <div key={i} className="space-y-3">
                  <div className="flex justify-between items-center">
                    <span className="font-bold text-apple-dark">{item.label}</span>
                    <span className={`font-black ${item.color}`}>{item.score}%</span>
                  </div>
                  <div className="h-2 w-full bg-gray-100 rounded-full overflow-hidden">
                    <div 
                      className={`h-full bg-current ${item.color}`} 
                      style={{ width: `${item.score}%` }} 
                    />
                  </div>
                </div>
              ))}
              
              <div className="md:col-span-2 pt-6 border-t border-gray-100">
                <h3 className="font-bold text-apple-dark mb-4 flex items-center">
                  <svg className="w-5 h-5 mr-2 text-yellow-500" fill="currentColor" viewBox="0 0 20 20"><path d="M11.3 1.046A1 1 0 0112 2v5h4a1 1 0 01.82 1.573l-7 10A1 1 0 018 18v-5H4a1 1 0 01-.82-1.573l7-10a1 1 0 011.12-.38z"></path></svg>
                  Gợi ý quan trọng từ AI
                </h3>
                <div className="space-y-4">
                  <div className="flex items-start space-x-3 p-4 bg-yellow-50 rounded-2xl border border-yellow-100">
                    <span className="text-yellow-600 mt-1">&bull;</span>
                    <p className="text-[15px] text-yellow-800 leading-relaxed">
                      Phần "Kỹ năng" nên được bổ sung thêm các công nghệ mới như <strong>React 19</strong> và <strong>Next.js App Router</strong> để phù hợp hơn với JD.
                    </p>
                  </div>
                  <div className="flex items-start space-x-3 p-4 bg-blue-50 rounded-2xl border border-blue-100">
                    <span className="text-blue-600 mt-1">&bull;</span>
                    <p className="text-[15px] text-blue-800 leading-relaxed">
                      Sử dụng các động từ mạnh (Action Verbs) ở đầu mỗi dòng kinh nghiệm để tăng tính thuyết phục.
                    </p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-mesh pt-32 pb-24 px-6">
      <div className="max-w-6xl mx-auto">
        {/* Header */}
        <div className="mb-12 animate-fade-in-up">
          <h1 className="text-[42px] font-bold text-apple-dark tracking-tight mb-4">AI CV Scoring</h1>
          <p className="text-apple-silver text-[19px] max-w-2xl">
            Tải CV của bạn lên và để trí tuệ nhân tạo đánh giá độ phù hợp với tiêu chuẩn ngành nghề toàn cầu.
          </p>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          {/* Main Input Area */}
          <div className="lg:col-span-2 space-y-8">
            {/* Upload Zone */}
            <div className="glass-card rounded-[32px] p-10 border-2 border-dashed border-apple-blue/20 hover:border-apple-blue/40 transition-all group relative overflow-hidden">
              <input 
                type="file" 
                onChange={handleFileChange}
                className="absolute inset-0 opacity-0 cursor-pointer z-20"
                accept=".pdf,.docx"
              />
              <div className="text-center space-y-4">
                <div className="w-20 h-20 bg-apple-blue/10 rounded-full flex items-center justify-center mx-auto group-hover:scale-110 transition-transform duration-500">
                  <svg width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="#0071e3" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="17 8 12 3 7 8"></polyline><line x1="12" y1="3" x2="12" y2="15"></line></svg>
                </div>
                <div>
                  <p className="text-xl font-bold text-apple-dark">
                    {file ? file.name : 'Kéo thả CV của bạn vào đây'}
                  </p>
                  <p className="text-apple-silver mt-2 text-sm">Hỗ trợ PDF, DOCX (Tối đa 5MB)</p>
                </div>
                {!file && (
                  <button className="px-6 py-2 bg-apple-blue text-white rounded-full font-semibold text-sm">
                    Chọn file
                  </button>
                )}
              </div>
            </div>

            {/* JD Input Area */}
            <div className="glass-card rounded-[32px] p-8 space-y-4">
               <div className="flex items-center space-x-2 mb-2">
                  <div className="w-8 h-8 rounded-lg bg-accent-indigo/10 flex items-center justify-center">
                    <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="#6366f1" strokeWidth="2"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"></path><polyline points="14 2 14 8 20 8"></polyline><line x1="16" y1="13" x2="8" y2="13"></line><line x1="16" y1="17" x2="8" y2="17"></line><polyline points="10 9 9 9 8 9"></polyline></svg>
                  </div>
                  <h3 className="text-lg font-bold text-apple-dark">Mô tả công việc (Tùy chọn)</h3>
               </div>
               <textarea 
                  value={jd}
                  onChange={(e) => setJd(e.target.value)}
                  placeholder="Dán mô tả công việc (JD) vào đây để AI so khớp mức độ phù hợp..."
                  className="w-full h-40 p-6 bg-apple-gray/50 rounded-2xl border-none focus:ring-2 focus:ring-apple-blue/20 outline-none text-[16px] leading-relaxed resize-none transition-all"
               />
            </div>

            {/* Action Button */}
            <button 
              onClick={handleAnalyze}
              disabled={!file || isAnalyzing}
              className={`w-full py-5 rounded-[24px] font-bold text-xl transition-all duration-500 shadow-xl flex items-center justify-center space-x-3
                ${!file || isAnalyzing 
                  ? 'bg-gray-200 text-gray-400 cursor-not-allowed shadow-none' 
                  : 'bg-apple-dark text-white hover:scale-[1.02] active:scale-95 shadow-apple-dark/20 hover:shadow-apple-dark/40'}
              `}
            >
              {isAnalyzing ? (
                <>
                  <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-white"></div>
                  <span>AI đang phân tích...</span>
                </>
              ) : (
                <>
                  <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"></path></svg>
                  <span>Bắt đầu chấm điểm bằng AI</span>
                </>
              )}
            </button>
          </div>

          {/* Tips Sidebar */}
          <div className="space-y-6">
            <div className="glass-card rounded-[32px] p-8 bg-gradient-to-br from-apple-blue/5 to-transparent border-apple-blue/10">
              <h3 className="text-lg font-bold text-apple-dark mb-4">Mẹo tối ưu CV</h3>
              <ul className="space-y-4">
                {[
                  'Sử dụng các từ khóa chuyên ngành.',
                  'Định dạng file PDF để giữ nguyên layout.',
                  'Tập trung vào các con số định lượng kết quả.',
                  'Đảm bảo thông tin liên hệ chính xác.'
                ].map((tip, i) => (
                  <li key={i} className="flex items-start space-x-3 text-[15px] text-apple-silver leading-snug">
                    <div className="mt-1 w-1.5 h-1.5 rounded-full bg-apple-blue flex-shrink-0" />
                    <span>{tip}</span>
                  </li>
                ))}
              </ul>
            </div>

            <div className="glass-card rounded-[32px] p-8 border-accent-purple/10 bg-gradient-to-br from-accent-purple/5 to-transparent">
              <h3 className="text-lg font-bold text-apple-dark mb-2">Bảo mật thông tin</h3>
              <p className="text-[14px] text-apple-silver leading-relaxed">
                CV của bạn được mã hóa và bảo vệ. Chúng tôi cam kết không chia sẻ dữ liệu cá nhân với bên thứ ba.
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CVScoringPage;
