using CVScore.Application.CvDocuments.GetUserCvDocuments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVScore.API.Controllers;

[Route("api/cv-documents")]
public class CvDocumentsController(ISender sender) : ApiControllerBase
{
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetUserCvDocumentsQuery(userId), cancellationToken);
        return FromResult(result);
    }
}
