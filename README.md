# 🤖 CVScore AI System - Mock Interview Platform

**CVScore AI System** là một nền tảng phỏng vấn giả định (Mock Interview) đột phá, kết hợp sức mạnh của **Google Gemini AI** và kiến trúc phần mềm sạch (**Clean Architecture**) để giúp ứng viên tối ưu hóa kỹ năng phỏng vấn dựa trên chính CV của họ.

---

## 🌟 Tính năng cốt lõi

- 📄 **Phân tích CV thông minh:** Tự động trích xuất kỹ năng, kinh nghiệm và dự án từ file PDF/DOC bằng Google Gemini Vision/Pro.
- ⌨️ **CV Builder (Overleaf-style):** Trình soạn thảo CV bằng code (JSON/Markdown) với tính năng xem trước thời gian thực.
- 🔍 **AI CV Reviewer:** Tự động kiểm tra lỗi chính tả, ngữ pháp và đánh giá CV dựa trên bộ tiêu chuẩn tùy chỉnh (Custom Standards).
- 🎯 **Cá nhân hóa câu hỏi:** AI sinh bộ câu hỏi "may đo" dựa trên sự kết hợp giữa CV, Vị trí (Role) và Cấp độ (Level) ứng tuyển.
- 🎙️ **Mô phỏng phỏng vấn thực tế:** Trải nghiệm trả lời câu hỏi theo trình tự từ cơ bản đến nâng cao.
- 📊 **Đánh giá chuyên sâu (AI Evaluation):** Chấm điểm, nhận xét chi tiết từng câu trả lời và gợi ý đáp án tối ưu thông qua mô hình Gemini.
- 🚀 **Xử lý nền (Background Processing):** Tối ưu hóa hiệu suất thông qua việc xử lý dữ liệu CV và đánh giá bằng các tác vụ chạy ngầm.

---

## 🏗️ Kiến trúc hệ thống (Clean Architecture)

Dự án được xây dựng dựa trên nguyên lý **Clean Architecture**, chia làm 4 lớp chính:

1.  **Domain (Entities & Business Rules):** Chứa các thực thể cốt lõi (User, CV, Interview...) và logic nghiệp vụ thuần túy.
2.  **Application (Use Cases):** Điều phối luồng dữ liệu và thực thi các nghiệp vụ (AnalyzeCV, GenerateQuestions...).
3.  **Infrastructure (External Services):** Thực thi các Interface từ lớp Application. Bao gồm cấu hình DB (**MySQL**), gọi API AI (**Google Gemini**), lưu trữ file.
4.  **Presentation (Web API & UI):** ASP.NET Core Web API và giao diện React hiện đại.

---

## 🛠️ Công nghệ sử dụng

| Thành phần | Công nghệ |
| :--- | :--- |
| **Backend** | .NET 8 / ASP.NET Core Web API |
| **Database** | **MySQL** |
| **ORM** | Entity Framework Core (Pomelo MySQL Provider) |
| **AI Service** | **Google Gemini API** (Gemini 1.5 Pro/Flash) |
| **Frontend** | React (TypeScript), TailwindCSS |
| **Giao tiếp Real-time** | SignalR |

---

## 🗄️ Thiết kế Cơ sở dữ liệu (ERD)

Hệ thống sử dụng **MySQL** với các mối quan hệ logic:
- **InterviewProfile:** Điểm hội tụ giữa dữ liệu CV và Form ứng tuyển.
- **InterviewQuestion:** Tách biệt câu hỏi thực tế trong phiên phỏng vấn.
- **Feedback:** Lưu trữ kết quả đánh giá từ Gemini AI độc lập với câu trả lời.

---

## 📂 Cấu trúc thư mục

```text
cvscore-ai-system/
├── backend/                  # ASP.NET Core Clean Architecture
│   ├── src/
│   │   ├── CVScore.Domain/       # Entities & Domain Logic
│   │   ├── CVScore.Application/  # Use Cases & Interfaces
│   │   ├── CVScore.Infrastructure/ # MySQL & Gemini Implementation
│   │   └── CVScore.API/          # Web API Endpoints
│   └── CVScore.sln
├── frontend/                 # React UI Application
│   ├── src/
│   ├── public/
│   └── package.json
└── README.md
```

---

## 🚀 Hướng dẫn cài đặt

### Yêu cầu hệ thống
- .NET 8 SDK
- MySQL Server (v8.0+)
- Node.js (v18+)
- Google AI (Gemini) API Key

### Các bước thực hiện
1. **Clone dự án:**
   ```bash
   git clone https://github.com/yourusername/cvscore-ai-system.git
   ```
2. **Cấu hình Backend:**
   Cập nhật `appsettings.json` trong `CVScore.API`:
   - ConnectionString cho MySQL.
   - Google AI Key cho Gemini.
3. **Chạy Migration:**
   ```bash
   dotnet ef database update
   ```
4. **Chạy ứng dụng:**
   - Backend: `dotnet run --project src/CVScore.API`
   - Frontend: `npm install && npm start`

---

- [x] Hệ thống Core Mock Interview (AI-based).
- [ ] **CV Builder & Reviewer:** Soạn thảo code & Kiểm tra tiêu chuẩn/chính tả bằng AI.
- [ ] Tích hợp **Speech-to-Text** để phỏng vấn bằng giọng nói.
- [ ] Hỗ trợ phỏng vấn nhiều vòng (Multi-round interview).

---
*Phát triển với sự hỗ trợ của Google Gemini AI.*