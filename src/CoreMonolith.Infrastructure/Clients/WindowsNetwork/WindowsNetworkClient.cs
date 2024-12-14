using CoreMonolith.Application.Abstractions.Clients;
using CoreMonolith.Domain.RootFolderClients;

namespace CoreMonolith.Infrastructure.Clients.WindowsNetwork
{
    public class WindowsNetworkClient : IRootFolderContextClient
    {
        public List<FolderListingResult> GetFolderListing(string relativePath)
        {
            throw new NotImplementedException();
        }
    }
}
