﻿using CoreMonolith.Domain.Models.RootFolderClients;

namespace CoreMonolith.Application.Abstractions.Clients;

public interface IRootFolderContextClient
{
    public List<FolderListingResult> GetFolderListing(string relativePath);
}

