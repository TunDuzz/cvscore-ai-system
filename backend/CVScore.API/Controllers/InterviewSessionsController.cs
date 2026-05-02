using CVScore.API.Contracts.InterviewAnswers;
using CVScore.API.Contracts.InterviewSessions;
using CVScore.Application.InterviewAnswers.EvaluateInterviewAnswer;
using CVScore.Application.InterviewAnswers.SubmitInterviewAnswer;
using CVScore.Application.InterviewSessions.CompleteInterviewSession;
using CVScore.Application.InterviewSessions.GetInterviewSessionDetail;
using CVScore.Application.InterviewSessions.StartInterviewSession;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVScore.API.Controllers;

[Route("api/interview-sessions")]
public class InterviewSessionsController(ISender sender) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Start(StartInterviewSessionRequest request, CancellationToken cancellationToken)
    {
        var command = new StartInterviewSessionCommand(
            request.InterviewProfileId,
            request.Questions
                .Select(x => new StartInterviewSessionQuestionItem(
                    x.DisplayOrder,
                    x.Category,
                    x.Difficulty,
                    x.Content,
                    x.ExpectedAnswerPoints,
                    x.AiRationale))
                .ToArray());

        var result = await sender.Send(command, cancellationToken);
        return FromResult(result, id => new { id }, StatusCodes.Status201Created);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetInterviewSessionDetailQuery(id), cancellationToken);
        return FromNullableResult(result);
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(
        Guid id,
        CompleteInterviewSessionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CompleteInterviewSessionCommand(
            id,
            request.OverallScore,
            request.Summary);

        var result = await sender.Send(command, cancellationToken);
        return FromResult(result, StatusCodes.Status204NoContent);
    }

    [HttpPost("answers")]
    public async Task<IActionResult> SubmitAnswer(SubmitInterviewAnswerRequest request, CancellationToken cancellationToken)
    {
        var command = new SubmitInterviewAnswerCommand(
            request.InterviewQuestionId,
            request.Content,
            request.AudioUrl,
            request.DurationInSeconds);

        var result = await sender.Send(command, cancellationToken);
        return FromResult(result, id => new { id });
    }

    [HttpPost("answers/{id:guid}/evaluate")]
    public async Task<IActionResult> EvaluateAnswer(
        Guid id,
        EvaluateInterviewAnswerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new EvaluateInterviewAnswerCommand(
            id,
            request.Score,
            request.Strengths,
            request.Improvements,
            request.SuggestedAnswer,
            request.DetailedAnalysis);

        var result = await sender.Send(command, cancellationToken);
        return FromResult(result, id => new { id });
    }
}
