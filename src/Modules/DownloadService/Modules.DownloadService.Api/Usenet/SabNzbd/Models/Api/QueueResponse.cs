using System.Text.Json.Serialization;

namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

public record QueueResponse(
    [property: JsonPropertyName("queue")] Queue Queue
);

public record Queue(
    [property: JsonPropertyName("version")] string Version,
    [property: JsonPropertyName("paused")] bool Paused,
    [property: JsonPropertyName("pause_int")] string PauseInt,
    [property: JsonPropertyName("paused_all")] bool PausedAll,
    [property: JsonPropertyName("diskspace1")] string Diskspace1,
    [property: JsonPropertyName("diskspace2")] string Diskspace2,
    [property: JsonPropertyName("diskspace1_norm")] string Diskspace1Norm,
    [property: JsonPropertyName("diskspace2_norm")] string Diskspace2Norm,
    [property: JsonPropertyName("diskspacetotal1")] string Diskspacetotal1,
    [property: JsonPropertyName("diskspacetotal2")] string Diskspacetotal2,
    [property: JsonPropertyName("speedlimit")] string Speedlimit,
    [property: JsonPropertyName("speedlimit_abs")] string SpeedlimitAbs,
    [property: JsonPropertyName("have_warnings")] string HaveWarnings,
    [property: JsonPropertyName("finishaction")] object Finishaction,
    [property: JsonPropertyName("quota")] string Quota,
    [property: JsonPropertyName("have_quota")] bool HaveQuota,
    [property: JsonPropertyName("left_quota")] string LeftQuota,
    [property: JsonPropertyName("cache_art")] string CacheArt,
    [property: JsonPropertyName("cache_size")] string CacheSize,
    [property: JsonPropertyName("kbpersec")] string Kbpersec,
    [property: JsonPropertyName("speed")] string Speed,
    [property: JsonPropertyName("mbleft")] string Mbleft,
    [property: JsonPropertyName("mb")] string Mb,
    [property: JsonPropertyName("sizeleft")] string Sizeleft,
    [property: JsonPropertyName("size")] string Size,
    [property: JsonPropertyName("noofslots_total")] int NoofslotsTotal,
    [property: JsonPropertyName("noofslots")] int Noofslots,
    [property: JsonPropertyName("start")] int Start,
    [property: JsonPropertyName("limit")] int Limit,
    [property: JsonPropertyName("finish")] int Finish,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("timeleft")] string Timeleft,
    [property: JsonPropertyName("slots")] IReadOnlyList<QueueSlot> Slots
);

public record QueueSlot(
    [property: JsonPropertyName("index")] int Index,
    [property: JsonPropertyName("nzo_id")] string NzoId,
    [property: JsonPropertyName("unpackopts")] string Unpackopts,
    [property: JsonPropertyName("priority")] string Priority,
    [property: JsonPropertyName("script")] string Script,
    [property: JsonPropertyName("filename")] string Filename,
    [property: JsonPropertyName("labels")] IReadOnlyList<string> Labels,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("cat")] string Cat,
    [property: JsonPropertyName("mbleft")] string Mbleft,
    [property: JsonPropertyName("mb")] string Mb,
    [property: JsonPropertyName("size")] string Size,
    [property: JsonPropertyName("sizeleft")] string Sizeleft,
    [property: JsonPropertyName("percentage")] string Percentage,
    [property: JsonPropertyName("mbmissing")] string Mbmissing,
    [property: JsonPropertyName("direct_unpack")] object DirectUnpack,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("timeleft")] string Timeleft,
    [property: JsonPropertyName("avg_age")] string AvgAge
);
