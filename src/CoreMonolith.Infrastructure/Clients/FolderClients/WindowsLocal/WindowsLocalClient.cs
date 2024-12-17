using CoreMonolith.Application.Abstractions.Clients;
using CoreMonolith.Domain.Models.RootFolderClients;

namespace CoreMonolith.Infrastructure.Clients.FolderClients.WindowsLocal
{
    public class WindowsLocalClient : IRootFolderContextClient
    {
        public List<FolderListingResult> GetFolderListing(string relativePath)
        {
            throw new NotImplementedException();
        }
    }
}
