﻿namespace Modules.DownloadService.Api.Usenet.Models;
public enum PostProcessingOptions
{
    Default = -1,
    None = 0,
    Repair = 1,
    RepairUnpack = 2,
    RepairUnpackDelete = 3
}
