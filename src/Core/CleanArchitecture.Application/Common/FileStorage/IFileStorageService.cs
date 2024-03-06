using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.FileStorage;

public interface IFileStorageService
{
    public Task<string> UploadAsync<T>(FileUploadRequest? request, FileType supportedFileType, CancellationToken cancellationToken = default)
        where T : class;

    public void Remove(string? path);
}