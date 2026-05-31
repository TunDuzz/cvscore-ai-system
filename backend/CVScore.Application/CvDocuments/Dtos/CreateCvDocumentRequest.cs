namespace CVScore.Application.CvDocuments.Dtos;

public class CreateCvDocumentRequest
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public string? RawText { get; set; }
    public string? ParsedData { get; set; }
    public string? Summary { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime? AnalyzedAt { get; set; }
}
