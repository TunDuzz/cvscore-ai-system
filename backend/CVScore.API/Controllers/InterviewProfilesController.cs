using CVScore.API.Contracts.InterviewProfiles;
using CVScore.Application.InterviewProfiles.CreateInterviewProfile;
using CVScore.Application.InterviewProfiles.GetInterviewProfileById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVScore.API.Controllers;

[Route("api/interview-profiles")]
public class InterviewProfilesController(ISender sender) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateInterviewProfileRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateInterviewProfileCommand(
            request.UserId,
            request.CvDocumentId,
            request.TargetRole,
            request.TargetLevel,
            request.CompanyName,
            request.JobDescription,
            request.TechStack,
            request.FocusAreas,
            request.Notes);

        var result = await sender.Send(command, cancellationToken);
        return FromResult(result, id => new { id }, StatusCodes.Status201Created);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetInterviewProfileByIdQuery(id), cancellationToken);
        return FromNullableResult(result);
    }
}
