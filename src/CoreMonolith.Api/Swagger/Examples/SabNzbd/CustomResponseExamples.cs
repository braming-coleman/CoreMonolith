﻿using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Swashbuckle.AspNetCore.Filters;

namespace CoreMonolith.Api.Swagger.Examples.SabNzbd;

public class CustomResponseExamples : IMultipleExamplesProvider<object>
{
    public IEnumerable<SwaggerExample<object>> GetExamples()
    {
        yield return SwaggerExample.Create("mode=version", GetVersionResponse());

        yield return SwaggerExample.Create("mode=get_config", GetConfigResponse());
    }

    private static object GetVersionResponse()
    {
        return new VersionResponse(Version: "4.4.1");
    }

    private static object GetConfigResponse()
    {
        return new ConfigResponse(
            Config: new Config(
                Misc: new Misc(
                    ConfigConversionVersion: 1,
                    HelpfulWarnings: 1,
                    QueueComplete: "echo 'Queue Complete'",
                    QueueCompletePers: 0,
                    BandwidthPerc: 100,
                    RefreshRate: 5,
                    InterfaceSettings: "default",
                    QueueLimit: 20,
                    ConfigLock: 0,
                    NotifiedNewSkin: 0,
                    CheckNewRel: 1,
                    AutoBrowser: false,
                    Language: "en",
                    EnableHttpsVerification: 1,
                    Host: "localhost",
                    Port: "8080",
                    HttpsPort: "9090",
                    Username: "sabnzbd",
                    Password: "yourpassword",
                    BandwidthMax: "0",
                    CacheLimit: "1G",
                    WebDir: "/sabnzbd",
                    WebColor: "gold",
                    HttpsCert: "server.cert",
                    HttpsKey: "server.key",
                    HttpsChain: "",
                    EnableHttps: 0,
                    InetExposure: 0,
                    ApiKey: Guid.CreateVersion7().ToString(),
                    NzbKey: Guid.CreateVersion7().ToString(),
                    Socks5ProxyUrl: "",
                    Permissions: "777",
                    DownloadDir: "/downloads/incomplete",
                    DownloadFree: "0",
                    CompleteDir: "/downloads/complete",
                    CompleteFree: "0",
                    FulldiskAutoresume: 1,
                    ScriptDir: "/scripts",
                    NzbBackupDir: "/downloads/nzb_backup",
                    AdminDir: "/admin",
                    BackupDir: "/backup",
                    DirscanDir: "/watch",
                    DirscanSpeed: 5,
                    PasswordFile: "",
                    LogDir: "/logs",
                    MaxArtTries: 3,
                    TopOnly: 0,
                    SfvCheck: 0,
                    ScriptCanFail: 0,
                    EnableRecursive: 1,
                    FlatUnpack: 0,
                    ParOption: "",
                    PreCheck: 0,
                    Nice: "",
                    WinProcessPrio: 3,
                    Ionice: "",
                    FailHopelessJobs: 0,
                    FastFail: 0,
                    AutoDisconnect: 0,
                    PreScript: "",
                    EndQueueScript: "",
                    NoDupes: 0,
                    NoSeriesDupes: 0,
                    NoSmartDupes: 0,
                    DupesPropercheck: 1,
                    PauseOnPwrar: 0,
                    IgnoreSamples: 0,
                    DeobfuscateFinalFilenames: 1,
                    AutoSort: "0",
                    DirectUnpack: 0,
                    PropagationDelay: 0,
                    FolderRename: 1,
                    ReplaceSpaces: 0,
                    ReplaceUnderscores: 0,
                    ReplaceDots: 0,
                    SafePostproc: 1,
                    PauseOnPostProcessing: 0,
                    EnableAllPar: 1,
                    SanitizeSafe: 1,
                    CleanupList: new List<object>(),
                    UnwantedExtensions: new List<object>(),
                    ActionOnUnwantedExtensions: 0,
                    UnwantedExtensionsMode: 0,
                    NewNzbOnFailure: 0,
                    HistoryRetention: "0",
                    HistoryRetentionOption: "0",
                    HistoryRetentionNumber: 0,
                    QuotaSize: "0",
                    QuotaDay: "0",
                    QuotaResume: 0,
                    QuotaPeriod: "day",
                    Schedlines: new List<object>(),
                    RssRate: 60,
                    Ampm: 0,
                    StartPaused: 0,
                    PreservePausedState: 0,
                    EnableParCleanup: 1,
                    ProcessUnpackedPar2: 1,
                    DisablePar2cmdline: 0,
                    EnableUnrar: 1,
                    Enable7zip: 1,
                    EnableFilejoin: 1,
                    EnableTsjoin: 1,
                    OverwriteFiles: 0,
                    IgnoreUnrarDates: 0,
                    BackupForDuplicates: 0,
                    EmptyPostproc: 0,
                    WaitForDfolder: 0,
                    RssFilenames: 0,
                    ApiLogging: 0,
                    HtmlLogin: 0,
                    DisableArchive: 0,
                    WarnDuplJobs: 1,
                    KeepAwake: 0,
                    TrayIcon: 0,
                    AllowIncompleteNzb: 0,
                    EnableBroadcast: 0,
                    Ipv6Hosting: 0,
                    Ipv6Staging: 0,
                    ApiWarnings: 1,
                    NoPenalties: 0,
                    XFrameOptions: 0,
                    AllowOldSslTls: 0,
                    EnableSeasonSorting: 0,
                    VerifyXffHeader: 0,
                    RssOddTitles: new List<string> { "ettv", "rartv" },
                    QuickCheckExtIgnore: new List<string> { ".txt", ".nfo", ".sfv" },
                    ReqCompletionRate: 95.0,
                    SelftestHost: "selftest.sabnzbd.org",
                    MovieRenameLimit: "0",
                    EpisodeRenameLimit: "0",
                    SizeLimit: "0",
                    DirectUnpackThreads: 2,
                    HistoryLimit: 100,
                    WaitExtDrive: 0,
                    MaxFoldernameLength: 250,
                    NomediaMarker: ".nomedia",
                    Ipv6Servers: 0,
                    UrlBase: "/",
                    HostWhitelist: new List<string> { "localhost", "127.0.0.1", "[::1]" },
                    LocalRanges: new List<object>(),
                    MaxUrlRetries: 3,
                    DownloaderSleepTime: 1,
                    ReceiveThreads: 8,
                    Switchinterval: 0.5,
                    SsdpBroadcastInterval: 600,
                    ExtRenameIgnore: new List<object>(),
                    EmailServer: "",
                    EmailTo: new List<object>(),
                    EmailFrom: "",
                    EmailAccount: "",
                    EmailPwd: "",
                    EmailEndjob: 0,
                    EmailFull: 0,
                    EmailDir: "",
                    EmailRss: 0,
                    EmailCats: new List<string>()
                ),
                Logging: new Logging(
                    LogLevel: 2,
                    MaxLogSize: 5242880,
                    LogBackups: 5
                ),
                Ncenter: new Ncenter(
                    NcenterEnable: 0,
                    NcenterCats: new List<string>(),
                    NcenterPrioStartup: 1,
                    NcenterPrioDownload: 1,
                    NcenterPrioPauseResume: 1,
                    NcenterPrioPp: 1,
                    NcenterPrioComplete: 1,
                    NcenterPrioFailed: 1,
                    NcenterPrioDiskFull: 1,
                    NcenterPrioNewLogin: 1,
                    NcenterPrioWarning: 1,
                    NcenterPrioError: 1,
                    NcenterPrioQueueDone: 1,
                    NcenterPrioOther: 1
                ),
                Acenter: new Acenter(
                    AcenterEnable: 0,
                    AcenterCats: new List<string>(),
                    AcenterPrioStartup: 1,
                    AcenterPrioDownload: 1,
                    AcenterPrioPauseResume: 1,
                    AcenterPrioPp: 1,
                    AcenterPrioComplete: 1,
                    AcenterPrioFailed: 1,
                    AcenterPrioDiskFull: 1,
                    AcenterPrioNewLogin: 1,
                    AcenterPrioWarning: 1,
                    AcenterPrioError: 1,
                    AcenterPrioQueueDone: 1,
                    AcenterPrioOther: 1
                ),
                Ntfosd: new Ntfosd(
                    NtfosdEnable: 0,
                    NtfosdCats: new List<string>(),
                    NtfosdPrioStartup: 1,
                    NtfosdPrioDownload: 1,
                    NtfosdPrioPauseResume: 1,
                    NtfosdPrioPp: 1,
                    NtfosdPrioComplete: 1,
                    NtfosdPrioFailed: 1,
                    NtfosdPrioDiskFull: 1,
                    NtfosdPrioNewLogin: 1,
                    NtfosdPrioWarning: 1,
                    NtfosdPrioError: 1,
                    NtfosdPrioQueueDone: 1,
                    NtfosdPrioOther: 1
                ),
                Prowl: new Prowl(
                    ProwlEnable: 0,
                    ProwlCats: new List<string>(),
                    ProwlApikey: "",
                    ProwlPrioStartup: 1,
                    ProwlPrioDownload: 1,
                    ProwlPrioPauseResume: 1,
                    ProwlPrioPp: 1,
                    ProwlPrioComplete: 1,
                    ProwlPrioFailed: 1,
                    ProwlPrioDiskFull: 1,
                    ProwlPrioNewLogin: 1,
                    ProwlPrioWarning: 1,
                    ProwlPrioError: 1,
                    ProwlPrioQueueDone: 1,
                    ProwlPrioOther: 1
                ),
                Pushover: new Pushover(
                    PushoverToken: "",
                    PushoverUserkey: "",
                    PushoverDevice: "",
                    PushoverEmergencyExpire: 3600,
                    PushoverEmergencyRetry: 60,
                    PushoverEnable: 0,
                    PushoverCats: new List<string>(),
                    PushoverPrioStartup: 1,
                    PushoverPrioDownload: 1,
                    PushoverPrioPauseResume: 1,
                    PushoverPrioPp: 1,
                    PushoverPrioComplete: 1,
                    PushoverPrioFailed: 1,
                    PushoverPrioDiskFull: 1,
                    PushoverPrioNewLogin: 1,
                    PushoverPrioWarning: 1,
                    PushoverPrioError: 1,
                    PushoverPrioQueueDone: 1,
                    PushoverPrioOther: 1
                ),
                Pushbullet: new Pushbullet(
                    PushbulletEnable: 0,
                    PushbulletCats: new List<string>(),
                    PushbulletApikey: "",
                    PushbulletDevice: "",
                    PushbulletPrioStartup: 1,
                    PushbulletPrioDownload: 1,
                    PushbulletPrioPauseResume: 1,
                    PushbulletPrioPp: 1,
                    PushbulletPrioComplete: 1,
                    PushbulletPrioFailed: 1,
                    PushbulletPrioDiskFull: 1,
                    PushbulletPrioNewLogin: 1,
                    PushbulletPrioWarning: 1,
                    PushbulletPrioError: 1,
                    PushbulletPrioQueueDone: 1,
                    PushbulletPrioOther: 1
                ),
                Apprise: new Apprise(
                    AppriseEnable: 0,
                    AppriseCats: new List<string>(),
                    AppriseUrls: "",
                    AppriseTargetStartup: "",
                    AppriseTargetStartupEnable: 0,
                    AppriseTargetDownload: "",
                    AppriseTargetDownloadEnable: 0,
                    AppriseTargetPauseResume: "",
                    AppriseTargetPauseResumeEnable: 0,
                    AppriseTargetPp: "",
                    AppriseTargetPpEnable: 0,
                    AppriseTargetComplete: "",
                    AppriseTargetCompleteEnable: 0,
                    AppriseTargetFailed: "",
                    AppriseTargetFailedEnable: 0,
                    AppriseTargetDiskFull: "",
                    AppriseTargetDiskFullEnable: 0,
                    AppriseTargetNewLogin: "",
                    AppriseTargetNewLoginEnable: 0,
                    AppriseTargetWarning: "",
                    AppriseTargetWarningEnable: 0,
                    AppriseTargetError: "",
                    AppriseTargetErrorEnable: 0,
                    AppriseTargetQueueDone: "",
                    AppriseTargetQueueDoneEnable: 0,
                    AppriseTargetOther: "",
                    AppriseTargetOtherEnable: 0
                ),
                Nscript: new Nscript(
                    NscriptEnable: 0,
                    NscriptCats: new List<string>(),
                    NscriptScript: "",
                    NscriptParameters: "",
                    NscriptPrioStartup: 1,
                    NscriptPrioDownload: 1,
                    NscriptPrioPauseResume: 1,
                    NscriptPrioPp: 1,
                    NscriptPrioComplete: 1,
                    NscriptPrioFailed: 1,
                    NscriptPrioDiskFull: 1,
                    NscriptPrioNewLogin: 1,
                    NscriptPrioWarning: 1,
                    NscriptPrioError: 1,
                    NscriptPrioQueueDone: 1,
                    NscriptPrioOther: 1
                ),
                Servers: new List<Server>
                {
                    new Server(
                        Name: "UsenetServer",
                        Displayname: "UsenetServer",
                        Host: "news.usenetserver.com",
                        Port: 563,
                        Timeout: 60,
                        Username: "yourusername",
                        Password: "yourpassword",
                        Connections: 20,
                        Ssl: 1,
                        SslVerify: 1,
                        SslCiphers: "",
                        Enable: 1,
                        Required: 0,
                        Optional: 0,
                        Retention: 0,
                        ExpireDate: "",
                        Quota: "0",
                        UsageAtStart: 0,
                        Priority: 0,
                        Notes: ""
                    )
                },
                Categories: new List<Category>
                {
                    new Category(
                        Name: "*",
                        Order: 0,
                        Pp: "",
                        Script: "Default",
                        Dir: "",
                        Newzbin: "",
                        Priority: 0
                    ),
                    new Category(
                        Name: "tv",
                        Order: 1,
                        Pp: "",
                        Script: "sabtosickbeard.py",
                        Dir: "",
                        Newzbin: "",
                        Priority: 0
                    )
                }
            )
        );
    }
}