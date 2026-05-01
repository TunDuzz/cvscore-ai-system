using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Application.CvDocuments.GetUserCvDocuments;

public class GetUserCvDocumentsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetUserCvDocumentsQuery, Result<IReadOnlyCollection<CvDocumentItemDto>>>
{
    public async Task<Result<IReadOnlyCollection<CvDocumentItemDto>>> Handle(
        GetUserCvDocumentsQuery request,
        CancellationToken cancellationToken)
    {
        var items = await context.CvDocuments
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.IsPrimary)
            .ThenByDescending(x => x.CreatedAt)
            .Select(x => new CvDocumentItemDto(
                x.Id,
                x.Title,
                x.FileName,
                x.FileUrl,
                x.FileType,
                x.IsPrimary,
                x.CreatedAt,
                x.AnalyzedAt))
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyCollection<CvDocumentItemDto>>.Success(items);
    }
}
