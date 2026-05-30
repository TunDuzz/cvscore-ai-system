using CVScore.Application.CvDocuments.Dtos;
using CVScore.Application.CvDocuments.Services;
using Microsoft.AspNetCore.Mvc;

namespace CVScore.API.Controllers;

[Route("api/cv-documents")]
public class CvDocumentsController(ICvDocumentService cvDocumentService) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCvDocumentRequest request, CancellationToken cancellationToken)
    {
        var result = await cvDocumentService.CreateAsync(request, cancellationToken);
        return CreatedFromResult(nameof(GetByUserId), new { userId = request.UserId }, result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var documents = await cvDocumentService.GetByUserIdAsync(userId, cancellationToken);
        return Ok(documents);
    }
}
