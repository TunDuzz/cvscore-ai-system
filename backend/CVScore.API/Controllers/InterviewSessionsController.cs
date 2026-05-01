using CVScore.API.Contracts.InterviewAnswers;
using CVScore.API.Contracts.InterviewSessions;
using CVScore.Application.InterviewAnswers.SubmitInterviewAnswer;
using CVScore.Application.InterviewSessions.GetInterviewSessionDetail;
using CVScore.Application.InterviewSessions.StartInterviewSession;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVScore.API.Controllers;

[ApiController]
[Route("api/interview-sessions")]
public class InterviewSessionsController(ISender sender) : ControllerBase
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
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, new { id = result.Value })
            : BadRequest(result.Error);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetInterviewSessionDetailQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
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
        return result.IsSuccess ? Ok(new { id = result.Value }) : BadRequest(result.Error);
    }
}
