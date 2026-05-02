using CVScore.Application.InterviewSessions.GenerateInterviewQuestions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVScore.API.Controllers;

[Route("api/interview-question-generation")]
public class InterviewQuestionGenerationController(ISender sender) : ApiControllerBase
{
    [HttpGet("profiles/{interviewProfileId:guid}")]
    public async Task<IActionResult> Generate(
        Guid interviewProfileId,
        [FromQuery] int questionCount = 5,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(
            new GenerateInterviewQuestionsQuery(interviewProfileId, questionCount),
            cancellationToken);

        return FromResult(result);
    }
}
