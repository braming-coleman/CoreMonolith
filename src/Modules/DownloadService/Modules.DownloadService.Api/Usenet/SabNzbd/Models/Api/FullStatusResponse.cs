using System.Text.Json.Serialization;

namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

public record FullStatusResponse(
    [property: JsonPropertyName("status")] Status Status
);

public record StatusServer(
    [property: JsonPropertyName("servername")] string Servername,
    [property: JsonPropertyName("serveractive")] bool Serveractive,
    [property: JsonPropertyName("serveractiveconn")] int Serveractiveconn,
    [property: JsonPropertyName("servertotalconn")] int Servertotalconn,
    [property: JsonPropertyName("serverconnections")] IReadOnlyList<Serverconnection> Serverconnections,
    [property: JsonPropertyName("serverssl")] int Serverssl,
    [property: JsonPropertyName("serversslinfo")] string Serversslinfo,
    [property: JsonPropertyName("serveripaddress")] string Serveripaddress,
    [property: JsonPropertyName("servercanonname")] string Servercanonname,
    [property: JsonPropertyName("serverwarning")] string Serverwarning,
    [property: JsonPropertyName("servererror")] string Servererror,
    [property: JsonPropertyName("serverpriority")] int Serverpriority,
    [property: JsonPropertyName("serveroptional")] int Serveroptional,
    [property: JsonPropertyName("serverbps")] string Serverbps
);

public record Serverconnection(
    [property: JsonPropertyName("thrdnum")] int Thrdnum,
    [property: JsonPropertyName("art_name")] string ArtName,
    [property: JsonPropertyName("nzf_name")] string NzfName,
    [property: JsonPropertyName("nzo_name")] string NzoName
);

public record Status(
    [property: JsonPropertyName("uptime")] string Uptime,
    [property: JsonPropertyName("color_scheme")] string ColorScheme,
    [property: JsonPropertyName("confighelpuri")] string Confighelpuri,
    [property: JsonPropertyName("pid")] int Pid,
    [property: JsonPropertyName("active_lang")] string ActiveLang,
    [property: JsonPropertyName("rtl")] bool Rtl,
    [property: JsonPropertyName("my_lcldata")] string MyLcldata,
    [property: JsonPropertyName("my_home")] string MyHome,
    [property: JsonPropertyName("webdir")] string Webdir,
    [property: JsonPropertyName("url_base")] string UrlBase,
    [property: JsonPropertyName("windows")] bool Windows,
    [property: JsonPropertyName("macos")] bool Macos,
    [property: JsonPropertyName("power_options")] bool PowerOptions,
    [property: JsonPropertyName("pp_pause_event")] bool PpPauseEvent,
    [property: JsonPropertyName("apikey")] string Apikey,
    [property: JsonPropertyName("new_release")] object NewRelease,
    [property: JsonPropertyName("new_rel_url")] object NewRelUrl,
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
    [property: JsonPropertyName("logfile")] string Logfile,
    [property: JsonPropertyName("weblogfile")] object Weblogfile,
    [property: JsonPropertyName("loglevel")] string Loglevel,
    [property: JsonPropertyName("folders")] IReadOnlyList<string> Folders,
    [property: JsonPropertyName("configfn")] string Configfn,
    [property: JsonPropertyName("warnings")] IReadOnlyList<Warning> Warnings,
    [property: JsonPropertyName("delayed_assembler")] int DelayedAssembler,
    [property: JsonPropertyName("loadavg")] string Loadavg,
    [property: JsonPropertyName("pystone")] int Pystone,
    [property: JsonPropertyName("downloaddir")] string Downloaddir,
    [property: JsonPropertyName("downloaddirspeed")] int Downloaddirspeed,
    [property: JsonPropertyName("completedir")] string Completedir,
    [property: JsonPropertyName("completedirspeed")] int Completedirspeed,
    [property: JsonPropertyName("internetbandwidth")] int Internetbandwidth,
    [property: JsonPropertyName("active_socks5_proxy")] object ActiveSocks5Proxy,
    [property: JsonPropertyName("localipv4")] string Localipv4,
    [property: JsonPropertyName("publicipv4")] string Publicipv4,
    [property: JsonPropertyName("ipv6")] object Ipv6,
    [property: JsonPropertyName("dnslookup")] bool Dnslookup,
    [property: JsonPropertyName("servers")] IReadOnlyList<StatusServer> Servers
);

public record Warning(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("time")] int Time,
    [property: JsonPropertyName("origin")] string Origin
);

