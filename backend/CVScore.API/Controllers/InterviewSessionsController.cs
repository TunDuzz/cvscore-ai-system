using CVScore.Application.InterviewSessions.Dtos;
using CVScore.Application.InterviewSessions.Services;
using Microsoft.AspNetCore.Mvc;

namespace CVScore.API.Controllers;

[Route("api/interview-sessions")]
public class InterviewSessionsController(IInterviewSessionService interviewSessionService) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Start([FromBody] StartInterviewSessionRequest request, CancellationToken cancellationToken)
    {
        var result = await interviewSessionService.StartAsync(request, cancellationToken);
        return CreatedFromResult(nameof(GetById), new { id = result.Data?.Id ?? Guid.Empty }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await interviewSessionService.GetByIdAsync(id, cancellationToken);
        return FromResult(result);
    }

    [HttpPost("answers")]
    public async Task<IActionResult> SubmitAnswer([FromBody] SubmitInterviewAnswerRequest request, CancellationToken cancellationToken)
    {
        var result = await interviewSessionService.SubmitAnswerAsync(request, cancellationToken);
        return FromResult(result);
    }

    [HttpPost("answers/{answerId:guid}/evaluate")]
    public async Task<IActionResult> EvaluateAnswer(Guid answerId, [FromBody] EvaluateInterviewAnswerRequest request, CancellationToken cancellationToken)
    {
        var result = await interviewSessionService.EvaluateAnswerAsync(answerId, request, cancellationToken);
        return FromResult(result);
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id, [FromBody] CompleteInterviewSessionRequest request, CancellationToken cancellationToken)
    {
        var result = await interviewSessionService.CompleteAsync(id, request, cancellationToken);
        return FromResult(result);
    }
}
