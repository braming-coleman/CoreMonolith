using CoreMonolith.Application.Abstractions.Clients;
using CoreMonolith.Domain.RootFolderClients;

namespace CoreMonolith.Infrastructure.Clients.Sftp
{
    public class SftpClient : IRootFolderContextClient
    {
        public List<FolderListingResult> GetFolderListing(string relativePath)
        {
            throw new NotImplementedException();
        }
    }
}
