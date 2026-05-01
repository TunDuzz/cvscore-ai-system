using CVScore.Application.CvDocuments.GetUserCvDocuments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVScore.API.Controllers;

[ApiController]
[Route("api/cv-documents")]
public class CvDocumentsController(ISender sender) : ControllerBase
{
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetUserCvDocumentsQuery(userId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
