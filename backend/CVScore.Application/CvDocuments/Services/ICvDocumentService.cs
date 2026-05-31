using CVScore.Application.Common;
using CVScore.Application.CvDocuments.Dtos;

namespace CVScore.Application.CvDocuments.Services;

public interface ICvDocumentService
{
    Task<ServiceResult<CvDocumentDto>> CreateAsync(CreateCvDocumentRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CvDocumentDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
