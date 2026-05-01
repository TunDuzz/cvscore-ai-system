using CVScore.Application.Common;
using MediatR;

namespace CVScore.Application.CvDocuments.GetUserCvDocuments;

public sealed record GetUserCvDocumentsQuery(Guid UserId) : IRequest<Result<IReadOnlyCollection<CvDocumentItemDto>>>;

public sealed record CvDocumentItemDto(
    Guid Id,
    string Title,
    string FileName,
    string FileUrl,
    string FileType,
    bool IsPrimary,
    DateTime CreatedAt,
    DateTime? AnalyzedAt);
