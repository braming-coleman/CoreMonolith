namespace CoreMonolith.Domain.RootFolderClients
{
    public class FolderListingResult
    {
        public List<FolderList> FolderListings { get; set; } = [];
        public List<FileListing> FileListings { get; set; } = [];
    }
}
