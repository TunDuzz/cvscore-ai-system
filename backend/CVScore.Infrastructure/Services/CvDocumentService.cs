using CVScore.Application.Common;
using CVScore.Application.CvDocuments.Dtos;
using CVScore.Application.CvDocuments.Services;
using CVScore.Domain.Entities;
using CVScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Infrastructure.Services;

public class CvDocumentService(ApplicationDbContext dbContext) : ICvDocumentService
{
    public async Task<ServiceResult<CvDocumentDto>> CreateAsync(CreateCvDocumentRequest request, CancellationToken cancellationToken = default)
    {
        if (request.UserId == Guid.Empty)
        {
            return ServiceResult<CvDocumentDto>.Failure("validation_error", "UserId is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Title) ||
            string.IsNullOrWhiteSpace(request.FileName) ||
            string.IsNullOrWhiteSpace(request.FileUrl) ||
            string.IsNullOrWhiteSpace(request.FileType))
        {
            return ServiceResult<CvDocumentDto>.Failure("validation_error", "Title, file name, file URL, and file type are required.");
        }

        var userExists = await dbContext.Users.AnyAsync(x => x.Id == request.UserId, cancellationToken);
        if (!userExists)
        {
            return ServiceResult<CvDocumentDto>.Failure("not_found", "User was not found.");
        }

        if (request.IsPrimary)
        {
            var currentPrimaryDocuments = await dbContext.CvDocuments
                .Where(x => x.UserId == request.UserId && x.IsPrimary)
                .ToListAsync(cancellationToken);

            foreach (var existingDocument in currentPrimaryDocuments)
            {
                existingDocument.IsPrimary = false;
                existingDocument.UpdatedAt = DateTime.UtcNow;
            }
        }

        var document = new CvDocument
        {
            UserId = request.UserId,
            Title = request.Title.Trim(),
            FileName = request.FileName.Trim(),
            FileUrl = request.FileUrl.Trim(),
            FileType = request.FileType.Trim(),
            RawText = request.RawText,
            ParsedData = request.ParsedData,
            Summary = request.Summary,
            IsPrimary = request.IsPrimary,
            AnalyzedAt = request.AnalyzedAt
        };

        dbContext.CvDocuments.Add(document);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ServiceResult<CvDocumentDto>.Success(Map(document));
    }

    public async Task<IReadOnlyList<CvDocumentDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.CvDocuments
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.IsPrimary)
            .ThenByDescending(x => x.CreatedAt)
            .Select(x => new CvDocumentDto
            {
                Id = x.Id,
                UserId = x.UserId,
                Title = x.Title,
                FileName = x.FileName,
                FileUrl = x.FileUrl,
                FileType = x.FileType,
                Summary = x.Summary,
                IsPrimary = x.IsPrimary,
                AnalyzedAt = x.AnalyzedAt,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }

    private static CvDocumentDto Map(CvDocument document) => new()
    {
        Id = document.Id,
        UserId = document.UserId,
        Title = document.Title,
        FileName = document.FileName,
        FileUrl = document.FileUrl,
        FileType = document.FileType,
        Summary = document.Summary,
        IsPrimary = document.IsPrimary,
        AnalyzedAt = document.AnalyzedAt,
        CreatedAt = document.CreatedAt
    };
}
