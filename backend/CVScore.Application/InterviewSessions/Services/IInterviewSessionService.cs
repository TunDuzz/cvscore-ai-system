using CVScore.Application.Common;
using CVScore.Application.InterviewSessions.Dtos;

namespace CVScore.Application.InterviewSessions.Services;

public interface IInterviewSessionService
{
    Task<ServiceResult<InterviewSessionDetailDto>> StartAsync(StartInterviewSessionRequest request, CancellationToken cancellationToken = default);
    Task<ServiceResult<InterviewSessionDetailDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ServiceResult<InterviewAnswerDto>> SubmitAnswerAsync(SubmitInterviewAnswerRequest request, CancellationToken cancellationToken = default);
    Task<ServiceResult<FeedbackDto>> EvaluateAnswerAsync(Guid answerId, EvaluateInterviewAnswerRequest request, CancellationToken cancellationToken = default);
    Task<ServiceResult<InterviewSessionDetailDto>> CompleteAsync(Guid sessionId, CompleteInterviewSessionRequest request, CancellationToken cancellationToken = default);
}
