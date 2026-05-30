using CVScore.Application.InterviewProfiles.Dtos;
using CVScore.Application.InterviewProfiles.Services;
using Microsoft.AspNetCore.Mvc;

namespace CVScore.API.Controllers;

[Route("api/interview-profiles")]
public class InterviewProfilesController(IInterviewProfileService interviewProfileService) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInterviewProfileRequest request, CancellationToken cancellationToken)
    {
        var result = await interviewProfileService.CreateAsync(request, cancellationToken);
        return CreatedFromResult(nameof(GetById), new { id = result.Data?.Id ?? Guid.Empty }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await interviewProfileService.GetByIdAsync(id, cancellationToken);
        return FromResult(result);
    }
}
