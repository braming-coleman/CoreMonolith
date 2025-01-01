using System.Text.Json.Serialization;

namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

public record HistoryResponse(
    [property: JsonPropertyName("history")] History History
);

public record History(
    [property: JsonPropertyName("total_size")] string TotalSize,
    [property: JsonPropertyName("month_size")] string MonthSize,
    [property: JsonPropertyName("week_size")] string WeekSize,
    [property: JsonPropertyName("day_size")] string DaySize,
    [property: JsonPropertyName("slots")] IReadOnlyList<HistorySlot> Slots,
    [property: JsonPropertyName("ppslots")] int Ppslots,
    [property: JsonPropertyName("noofslots")] int Noofslots,
    [property: JsonPropertyName("last_history_update")] int LastHistoryUpdate,
    [property: JsonPropertyName("version")] string Version
);

public record HistorySlot(
    [property: JsonPropertyName("completed")] int Completed,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("nzb_name")] string NzbName,
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("pp")] string Pp,
    [property: JsonPropertyName("script")] string Script,
    [property: JsonPropertyName("report")] string Report,
    [property: JsonPropertyName("url")] object Url,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("nzo_id")] string NzoId,
    [property: JsonPropertyName("storage")] string Storage,
    [property: JsonPropertyName("path")] string Path,
    [property: JsonPropertyName("script_line")] string ScriptLine,
    [property: JsonPropertyName("download_time")] int DownloadTime,
    [property: JsonPropertyName("postproc_time")] int PostprocTime,
    [property: JsonPropertyName("stage_log")] IReadOnlyList<StageLog> StageLog,
    [property: JsonPropertyName("downloaded")] long Downloaded,
    [property: JsonPropertyName("completeness")] object Completeness,
    [property: JsonPropertyName("fail_message")] string FailMessage,
    [property: JsonPropertyName("url_info")] string UrlInfo,
    [property: JsonPropertyName("bytes")] long Bytes,
    [property: JsonPropertyName("meta")] object Meta,
    [property: JsonPropertyName("series")] object Series,
    [property: JsonPropertyName("md5sum")] string Md5sum,
    [property: JsonPropertyName("password")] object Password,
    [property: JsonPropertyName("duplicate_key")] string DuplicateKey,
    [property: JsonPropertyName("archive")] bool Archive,
    [property: JsonPropertyName("size")] string Size,
    [property: JsonPropertyName("action_line")] string ActionLine,
    [property: JsonPropertyName("loaded")] bool Loaded,
    [property: JsonPropertyName("retry")] bool Retry
);

public record StageLog(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("actions")] IReadOnlyList<string> Actions
);

