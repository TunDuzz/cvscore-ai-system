import './index.css'

function App() {
  return (
    <div className="min-h-screen w-full bg-white flex flex-col items-center justify-center p-4">
      <div className="max-w-2xl text-center">
        <h1 className="text-5xl font-extrabold text-slate-900 mb-6 tracking-tight">
          CVScore AI System
        </h1>
        <p className="text-xl text-slate-600 mb-8 leading-relaxed">
          Nền tảng phỏng vấn giả định và xây dựng CV thông minh được hỗ trợ bởi Google Gemini AI.
        </p>
        <div className="flex gap-4 justify-center">
          <button className="px-6 py-3 bg-blue-600 text-white font-semibold rounded-lg shadow-md hover:bg-blue-700 transition duration-300">
            Bắt đầu tạo CV
          </button>
          <button className="px-6 py-3 bg-white text-blue-600 font-semibold border border-blue-600 rounded-lg hover:bg-blue-50 transition duration-300">
            Thử phỏng vấn AI
          </button>
        </div>
      </div>
    </div>
  )
}

export default App
