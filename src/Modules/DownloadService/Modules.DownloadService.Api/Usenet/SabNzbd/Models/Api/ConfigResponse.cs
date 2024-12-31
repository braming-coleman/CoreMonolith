﻿using System.Text.Json.Serialization;

namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

public record ConfigResponse(
    [property: JsonPropertyName("config")] Config Config
);

public record Acenter(
    [property: JsonPropertyName("acenter_enable")] int AcenterEnable,
    [property: JsonPropertyName("acenter_cats")] IReadOnlyList<string> AcenterCats,
    [property: JsonPropertyName("acenter_prio_startup")] int AcenterPrioStartup,
    [property: JsonPropertyName("acenter_prio_download")] int AcenterPrioDownload,
    [property: JsonPropertyName("acenter_prio_pause_resume")] int AcenterPrioPauseResume,
    [property: JsonPropertyName("acenter_prio_pp")] int AcenterPrioPp,
    [property: JsonPropertyName("acenter_prio_complete")] int AcenterPrioComplete,
    [property: JsonPropertyName("acenter_prio_failed")] int AcenterPrioFailed,
    [property: JsonPropertyName("acenter_prio_disk_full")] int AcenterPrioDiskFull,
    [property: JsonPropertyName("acenter_prio_new_login")] int AcenterPrioNewLogin,
    [property: JsonPropertyName("acenter_prio_warning")] int AcenterPrioWarning,
    [property: JsonPropertyName("acenter_prio_error")] int AcenterPrioError,
    [property: JsonPropertyName("acenter_prio_queue_done")] int AcenterPrioQueueDone,
    [property: JsonPropertyName("acenter_prio_other")] int AcenterPrioOther
);

public record Apprise(
    [property: JsonPropertyName("apprise_enable")] int AppriseEnable,
    [property: JsonPropertyName("apprise_cats")] IReadOnlyList<string> AppriseCats,
    [property: JsonPropertyName("apprise_urls")] string AppriseUrls,
    [property: JsonPropertyName("apprise_target_startup")] string AppriseTargetStartup,
    [property: JsonPropertyName("apprise_target_startup_enable")] int AppriseTargetStartupEnable,
    [property: JsonPropertyName("apprise_target_download")] string AppriseTargetDownload,
    [property: JsonPropertyName("apprise_target_download_enable")] int AppriseTargetDownloadEnable,
    [property: JsonPropertyName("apprise_target_pause_resume")] string AppriseTargetPauseResume,
    [property: JsonPropertyName("apprise_target_pause_resume_enable")] int AppriseTargetPauseResumeEnable,
    [property: JsonPropertyName("apprise_target_pp")] string AppriseTargetPp,
    [property: JsonPropertyName("apprise_target_pp_enable")] int AppriseTargetPpEnable,
    [property: JsonPropertyName("apprise_target_complete")] string AppriseTargetComplete,
    [property: JsonPropertyName("apprise_target_complete_enable")] int AppriseTargetCompleteEnable,
    [property: JsonPropertyName("apprise_target_failed")] string AppriseTargetFailed,
    [property: JsonPropertyName("apprise_target_failed_enable")] int AppriseTargetFailedEnable,
    [property: JsonPropertyName("apprise_target_disk_full")] string AppriseTargetDiskFull,
    [property: JsonPropertyName("apprise_target_disk_full_enable")] int AppriseTargetDiskFullEnable,
    [property: JsonPropertyName("apprise_target_new_login")] string AppriseTargetNewLogin,
    [property: JsonPropertyName("apprise_target_new_login_enable")] int AppriseTargetNewLoginEnable,
    [property: JsonPropertyName("apprise_target_warning")] string AppriseTargetWarning,
    [property: JsonPropertyName("apprise_target_warning_enable")] int AppriseTargetWarningEnable,
    [property: JsonPropertyName("apprise_target_error")] string AppriseTargetError,
    [property: JsonPropertyName("apprise_target_error_enable")] int AppriseTargetErrorEnable,
    [property: JsonPropertyName("apprise_target_queue_done")] string AppriseTargetQueueDone,
    [property: JsonPropertyName("apprise_target_queue_done_enable")] int AppriseTargetQueueDoneEnable,
    [property: JsonPropertyName("apprise_target_other")] string AppriseTargetOther,
    [property: JsonPropertyName("apprise_target_other_enable")] int AppriseTargetOtherEnable
);

public record Category(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("order")] int Order,
    [property: JsonPropertyName("pp")] string Pp,
    [property: JsonPropertyName("script")] string Script,
    [property: JsonPropertyName("dir")] string Dir,
    [property: JsonPropertyName("newzbin")] string Newzbin,
    [property: JsonPropertyName("priority")] int Priority
);

public record Config(
    [property: JsonPropertyName("misc")] Misc Misc,
    [property: JsonPropertyName("logging")] Logging Logging,
    [property: JsonPropertyName("ncenter")] Ncenter Ncenter,
    [property: JsonPropertyName("acenter")] Acenter Acenter,
    [property: JsonPropertyName("ntfosd")] Ntfosd Ntfosd,
    [property: JsonPropertyName("prowl")] Prowl Prowl,
    [property: JsonPropertyName("pushover")] Pushover Pushover,
    [property: JsonPropertyName("pushbullet")] Pushbullet Pushbullet,
    [property: JsonPropertyName("apprise")] Apprise Apprise,
    [property: JsonPropertyName("nscript")] Nscript Nscript,
    [property: JsonPropertyName("servers")] IReadOnlyList<Server> Servers,
    [property: JsonPropertyName("categories")] IReadOnlyList<Category> Categories
);

public record Logging(
    [property: JsonPropertyName("log_level")] int LogLevel,
    [property: JsonPropertyName("max_log_size")] int MaxLogSize,
    [property: JsonPropertyName("log_backups")] int LogBackups
);

public record Misc(
    [property: JsonPropertyName("config_conversion_version")] int ConfigConversionVersion,
    [property: JsonPropertyName("helpful_warnings")] int HelpfulWarnings,
    [property: JsonPropertyName("queue_complete")] string QueueComplete,
    [property: JsonPropertyName("queue_complete_pers")] int QueueCompletePers,
    [property: JsonPropertyName("bandwidth_perc")] int BandwidthPerc,
    [property: JsonPropertyName("refresh_rate")] int RefreshRate,
    [property: JsonPropertyName("interface_settings")] string InterfaceSettings,
    [property: JsonPropertyName("queue_limit")] int QueueLimit,
    [property: JsonPropertyName("config_lock")] int ConfigLock,
    [property: JsonPropertyName("notified_new_skin")] int NotifiedNewSkin,
    [property: JsonPropertyName("check_new_rel")] int CheckNewRel,
    [property: JsonPropertyName("auto_browser")] bool AutoBrowser,
    [property: JsonPropertyName("language")] string Language,
    [property: JsonPropertyName("enable_https_verification")] int EnableHttpsVerification,
    [property: JsonPropertyName("host")] string Host,
    [property: JsonPropertyName("port")] string Port,
    [property: JsonPropertyName("https_port")] string HttpsPort,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("bandwidth_max")] string BandwidthMax,
    [property: JsonPropertyName("cache_limit")] string CacheLimit,
    [property: JsonPropertyName("web_dir")] string WebDir,
    [property: JsonPropertyName("web_color")] string WebColor,
    [property: JsonPropertyName("https_cert")] string HttpsCert,
    [property: JsonPropertyName("https_key")] string HttpsKey,
    [property: JsonPropertyName("https_chain")] string HttpsChain,
    [property: JsonPropertyName("enable_https")] int EnableHttps,
    [property: JsonPropertyName("inet_exposure")] int InetExposure,
    [property: JsonPropertyName("api_key")] string ApiKey,
    [property: JsonPropertyName("nzb_key")] string NzbKey,
    [property: JsonPropertyName("socks5_proxy_url")] string Socks5ProxyUrl,
    [property: JsonPropertyName("permissions")] string Permissions,
    [property: JsonPropertyName("download_dir")] string DownloadDir,
    [property: JsonPropertyName("download_free")] string DownloadFree,
    [property: JsonPropertyName("complete_dir")] string CompleteDir,
    [property: JsonPropertyName("complete_free")] string CompleteFree,
    [property: JsonPropertyName("fulldisk_autoresume")] int FulldiskAutoresume,
    [property: JsonPropertyName("script_dir")] string ScriptDir,
    [property: JsonPropertyName("nzb_backup_dir")] string NzbBackupDir,
    [property: JsonPropertyName("admin_dir")] string AdminDir,
    [property: JsonPropertyName("backup_dir")] string BackupDir,
    [property: JsonPropertyName("dirscan_dir")] string DirscanDir,
    [property: JsonPropertyName("dirscan_speed")] int DirscanSpeed,
    [property: JsonPropertyName("password_file")] string PasswordFile,
    [property: JsonPropertyName("log_dir")] string LogDir,
    [property: JsonPropertyName("max_art_tries")] int MaxArtTries,
    [property: JsonPropertyName("top_only")] int TopOnly,
    [property: JsonPropertyName("sfv_check")] int SfvCheck,
    [property: JsonPropertyName("script_can_fail")] int ScriptCanFail,
    [property: JsonPropertyName("enable_recursive")] int EnableRecursive,
    [property: JsonPropertyName("flat_unpack")] int FlatUnpack,
    [property: JsonPropertyName("par_option")] string ParOption,
    [property: JsonPropertyName("pre_check")] int PreCheck,
    [property: JsonPropertyName("nice")] string Nice,
    [property: JsonPropertyName("win_process_prio")] int WinProcessPrio,
    [property: JsonPropertyName("ionice")] string Ionice,
    [property: JsonPropertyName("fail_hopeless_jobs")] int FailHopelessJobs,
    [property: JsonPropertyName("fast_fail")] int FastFail,
    [property: JsonPropertyName("auto_disconnect")] int AutoDisconnect,
    [property: JsonPropertyName("pre_script")] string PreScript,
    [property: JsonPropertyName("end_queue_script")] string EndQueueScript,
    [property: JsonPropertyName("no_dupes")] int NoDupes,
    [property: JsonPropertyName("no_series_dupes")] int NoSeriesDupes,
    [property: JsonPropertyName("no_smart_dupes")] int NoSmartDupes,
    [property: JsonPropertyName("dupes_propercheck")] int DupesPropercheck,
    [property: JsonPropertyName("pause_on_pwrar")] int PauseOnPwrar,
    [property: JsonPropertyName("ignore_samples")] int IgnoreSamples,
    [property: JsonPropertyName("deobfuscate_final_filenames")] int DeobfuscateFinalFilenames,
    [property: JsonPropertyName("auto_sort")] string AutoSort,
    [property: JsonPropertyName("direct_unpack")] int DirectUnpack,
    [property: JsonPropertyName("propagation_delay")] int PropagationDelay,
    [property: JsonPropertyName("folder_rename")] int FolderRename,
    [property: JsonPropertyName("replace_spaces")] int ReplaceSpaces,
    [property: JsonPropertyName("replace_underscores")] int ReplaceUnderscores,
    [property: JsonPropertyName("replace_dots")] int ReplaceDots,
    [property: JsonPropertyName("safe_postproc")] int SafePostproc,
    [property: JsonPropertyName("pause_on_post_processing")] int PauseOnPostProcessing,
    [property: JsonPropertyName("enable_all_par")] int EnableAllPar,
    [property: JsonPropertyName("sanitize_safe")] int SanitizeSafe,
    [property: JsonPropertyName("cleanup_list")] IReadOnlyList<object> CleanupList,
    [property: JsonPropertyName("unwanted_extensions")] IReadOnlyList<object> UnwantedExtensions,
    [property: JsonPropertyName("action_on_unwanted_extensions")] int ActionOnUnwantedExtensions,
    [property: JsonPropertyName("unwanted_extensions_mode")] int UnwantedExtensionsMode,
    [property: JsonPropertyName("new_nzb_on_failure")] int NewNzbOnFailure,
    [property: JsonPropertyName("history_retention")] string HistoryRetention,
    [property: JsonPropertyName("history_retention_option")] string HistoryRetentionOption,
    [property: JsonPropertyName("history_retention_number")] int HistoryRetentionNumber,
    [property: JsonPropertyName("quota_size")] string QuotaSize,
    [property: JsonPropertyName("quota_day")] string QuotaDay,
    [property: JsonPropertyName("quota_resume")] int QuotaResume,
    [property: JsonPropertyName("quota_period")] string QuotaPeriod,
    [property: JsonPropertyName("schedlines")] IReadOnlyList<object> Schedlines,
    [property: JsonPropertyName("rss_rate")] int RssRate,
    [property: JsonPropertyName("ampm")] int Ampm,
    [property: JsonPropertyName("start_paused")] int StartPaused,
    [property: JsonPropertyName("preserve_paused_state")] int PreservePausedState,
    [property: JsonPropertyName("enable_par_cleanup")] int EnableParCleanup,
    [property: JsonPropertyName("process_unpacked_par2")] int ProcessUnpackedPar2,
    [property: JsonPropertyName("disable_par2cmdline")] int DisablePar2cmdline,
    [property: JsonPropertyName("enable_unrar")] int EnableUnrar,
    [property: JsonPropertyName("enable_7zip")] int Enable7zip,
    [property: JsonPropertyName("enable_filejoin")] int EnableFilejoin,
    [property: JsonPropertyName("enable_tsjoin")] int EnableTsjoin,
    [property: JsonPropertyName("overwrite_files")] int OverwriteFiles,
    [property: JsonPropertyName("ignore_unrar_dates")] int IgnoreUnrarDates,
    [property: JsonPropertyName("backup_for_duplicates")] int BackupForDuplicates,
    [property: JsonPropertyName("empty_postproc")] int EmptyPostproc,
    [property: JsonPropertyName("wait_for_dfolder")] int WaitForDfolder,
    [property: JsonPropertyName("rss_filenames")] int RssFilenames,
    [property: JsonPropertyName("api_logging")] int ApiLogging,
    [property: JsonPropertyName("html_login")] int HtmlLogin,
    [property: JsonPropertyName("disable_archive")] int DisableArchive,
    [property: JsonPropertyName("warn_dupl_jobs")] int WarnDuplJobs,
    [property: JsonPropertyName("keep_awake")] int KeepAwake,
    [property: JsonPropertyName("tray_icon")] int TrayIcon,
    [property: JsonPropertyName("allow_incomplete_nzb")] int AllowIncompleteNzb,
    [property: JsonPropertyName("enable_broadcast")] int EnableBroadcast,
    [property: JsonPropertyName("ipv6_hosting")] int Ipv6Hosting,
    [property: JsonPropertyName("ipv6_staging")] int Ipv6Staging,
    [property: JsonPropertyName("api_warnings")] int ApiWarnings,
    [property: JsonPropertyName("no_penalties")] int NoPenalties,
    [property: JsonPropertyName("x_frame_options")] int XFrameOptions,
    [property: JsonPropertyName("allow_old_ssl_tls")] int AllowOldSslTls,
    [property: JsonPropertyName("enable_season_sorting")] int EnableSeasonSorting,
    [property: JsonPropertyName("verify_xff_header")] int VerifyXffHeader,
    [property: JsonPropertyName("rss_odd_titles")] IReadOnlyList<string> RssOddTitles,
    [property: JsonPropertyName("quick_check_ext_ignore")] IReadOnlyList<string> QuickCheckExtIgnore,
    [property: JsonPropertyName("req_completion_rate")] double ReqCompletionRate,
    [property: JsonPropertyName("selftest_host")] string SelftestHost,
    [property: JsonPropertyName("movie_rename_limit")] string MovieRenameLimit,
    [property: JsonPropertyName("episode_rename_limit")] string EpisodeRenameLimit,
    [property: JsonPropertyName("size_limit")] string SizeLimit,
    [property: JsonPropertyName("direct_unpack_threads")] int DirectUnpackThreads,
    [property: JsonPropertyName("history_limit")] int HistoryLimit,
    [property: JsonPropertyName("wait_ext_drive")] int WaitExtDrive,
    [property: JsonPropertyName("max_foldername_length")] int MaxFoldernameLength,
    [property: JsonPropertyName("nomedia_marker")] string NomediaMarker,
    [property: JsonPropertyName("ipv6_servers")] int Ipv6Servers,
    [property: JsonPropertyName("url_base")] string UrlBase,
    [property: JsonPropertyName("host_whitelist")] IReadOnlyList<string> HostWhitelist,
    [property: JsonPropertyName("local_ranges")] IReadOnlyList<object> LocalRanges,
    [property: JsonPropertyName("max_url_retries")] int MaxUrlRetries,
    [property: JsonPropertyName("downloader_sleep_time")] int DownloaderSleepTime,
    [property: JsonPropertyName("receive_threads")] int ReceiveThreads,
    [property: JsonPropertyName("switchinterval")] double Switchinterval,
    [property: JsonPropertyName("ssdp_broadcast_interval")] int SsdpBroadcastInterval,
    [property: JsonPropertyName("ext_rename_ignore")] IReadOnlyList<object> ExtRenameIgnore,
    [property: JsonPropertyName("email_server")] string EmailServer,
    [property: JsonPropertyName("email_to")] IReadOnlyList<object> EmailTo,
    [property: JsonPropertyName("email_from")] string EmailFrom,
    [property: JsonPropertyName("email_account")] string EmailAccount,
    [property: JsonPropertyName("email_pwd")] string EmailPwd,
    [property: JsonPropertyName("email_endjob")] int EmailEndjob,
    [property: JsonPropertyName("email_full")] int EmailFull,
    [property: JsonPropertyName("email_dir")] string EmailDir,
    [property: JsonPropertyName("email_rss")] int EmailRss,
    [property: JsonPropertyName("email_cats")] IReadOnlyList<string> EmailCats
);

public record Ncenter(
    [property: JsonPropertyName("ncenter_enable")] int NcenterEnable,
    [property: JsonPropertyName("ncenter_cats")] IReadOnlyList<string> NcenterCats,
    [property: JsonPropertyName("ncenter_prio_startup")] int NcenterPrioStartup,
    [property: JsonPropertyName("ncenter_prio_download")] int NcenterPrioDownload,
    [property: JsonPropertyName("ncenter_prio_pause_resume")] int NcenterPrioPauseResume,
    [property: JsonPropertyName("ncenter_prio_pp")] int NcenterPrioPp,
    [property: JsonPropertyName("ncenter_prio_complete")] int NcenterPrioComplete,
    [property: JsonPropertyName("ncenter_prio_failed")] int NcenterPrioFailed,
    [property: JsonPropertyName("ncenter_prio_disk_full")] int NcenterPrioDiskFull,
    [property: JsonPropertyName("ncenter_prio_new_login")] int NcenterPrioNewLogin,
    [property: JsonPropertyName("ncenter_prio_warning")] int NcenterPrioWarning,
    [property: JsonPropertyName("ncenter_prio_error")] int NcenterPrioError,
    [property: JsonPropertyName("ncenter_prio_queue_done")] int NcenterPrioQueueDone,
    [property: JsonPropertyName("ncenter_prio_other")] int NcenterPrioOther
);

public record Nscript(
    [property: JsonPropertyName("nscript_enable")] int NscriptEnable,
    [property: JsonPropertyName("nscript_cats")] IReadOnlyList<string> NscriptCats,
    [property: JsonPropertyName("nscript_script")] string NscriptScript,
    [property: JsonPropertyName("nscript_parameters")] string NscriptParameters,
    [property: JsonPropertyName("nscript_prio_startup")] int NscriptPrioStartup,
    [property: JsonPropertyName("nscript_prio_download")] int NscriptPrioDownload,
    [property: JsonPropertyName("nscript_prio_pause_resume")] int NscriptPrioPauseResume,
    [property: JsonPropertyName("nscript_prio_pp")] int NscriptPrioPp,
    [property: JsonPropertyName("nscript_prio_complete")] int NscriptPrioComplete,
    [property: JsonPropertyName("nscript_prio_failed")] int NscriptPrioFailed,
    [property: JsonPropertyName("nscript_prio_disk_full")] int NscriptPrioDiskFull,
    [property: JsonPropertyName("nscript_prio_new_login")] int NscriptPrioNewLogin,
    [property: JsonPropertyName("nscript_prio_warning")] int NscriptPrioWarning,
    [property: JsonPropertyName("nscript_prio_error")] int NscriptPrioError,
    [property: JsonPropertyName("nscript_prio_queue_done")] int NscriptPrioQueueDone,
    [property: JsonPropertyName("nscript_prio_other")] int NscriptPrioOther
);

public record Ntfosd(
    [property: JsonPropertyName("ntfosd_enable")] int NtfosdEnable,
    [property: JsonPropertyName("ntfosd_cats")] IReadOnlyList<string> NtfosdCats,
    [property: JsonPropertyName("ntfosd_prio_startup")] int NtfosdPrioStartup,
    [property: JsonPropertyName("ntfosd_prio_download")] int NtfosdPrioDownload,
    [property: JsonPropertyName("ntfosd_prio_pause_resume")] int NtfosdPrioPauseResume,
    [property: JsonPropertyName("ntfosd_prio_pp")] int NtfosdPrioPp,
    [property: JsonPropertyName("ntfosd_prio_complete")] int NtfosdPrioComplete,
    [property: JsonPropertyName("ntfosd_prio_failed")] int NtfosdPrioFailed,
    [property: JsonPropertyName("ntfosd_prio_disk_full")] int NtfosdPrioDiskFull,
    [property: JsonPropertyName("ntfosd_prio_new_login")] int NtfosdPrioNewLogin,
    [property: JsonPropertyName("ntfosd_prio_warning")] int NtfosdPrioWarning,
    [property: JsonPropertyName("ntfosd_prio_error")] int NtfosdPrioError,
    [property: JsonPropertyName("ntfosd_prio_queue_done")] int NtfosdPrioQueueDone,
    [property: JsonPropertyName("ntfosd_prio_other")] int NtfosdPrioOther
);

public record Prowl(
    [property: JsonPropertyName("prowl_enable")] int ProwlEnable,
    [property: JsonPropertyName("prowl_cats")] IReadOnlyList<string> ProwlCats,
    [property: JsonPropertyName("prowl_apikey")] string ProwlApikey,
    [property: JsonPropertyName("prowl_prio_startup")] int ProwlPrioStartup,
    [property: JsonPropertyName("prowl_prio_download")] int ProwlPrioDownload,
    [property: JsonPropertyName("prowl_prio_pause_resume")] int ProwlPrioPauseResume,
    [property: JsonPropertyName("prowl_prio_pp")] int ProwlPrioPp,
    [property: JsonPropertyName("prowl_prio_complete")] int ProwlPrioComplete,
    [property: JsonPropertyName("prowl_prio_failed")] int ProwlPrioFailed,
    [property: JsonPropertyName("prowl_prio_disk_full")] int ProwlPrioDiskFull,
    [property: JsonPropertyName("prowl_prio_new_login")] int ProwlPrioNewLogin,
    [property: JsonPropertyName("prowl_prio_warning")] int ProwlPrioWarning,
    [property: JsonPropertyName("prowl_prio_error")] int ProwlPrioError,
    [property: JsonPropertyName("prowl_prio_queue_done")] int ProwlPrioQueueDone,
    [property: JsonPropertyName("prowl_prio_other")] int ProwlPrioOther
);

public record Pushbullet(
    [property: JsonPropertyName("pushbullet_enable")] int PushbulletEnable,
    [property: JsonPropertyName("pushbullet_cats")] IReadOnlyList<string> PushbulletCats,
    [property: JsonPropertyName("pushbullet_apikey")] string PushbulletApikey,
    [property: JsonPropertyName("pushbullet_device")] string PushbulletDevice,
    [property: JsonPropertyName("pushbullet_prio_startup")] int PushbulletPrioStartup,
    [property: JsonPropertyName("pushbullet_prio_download")] int PushbulletPrioDownload,
    [property: JsonPropertyName("pushbullet_prio_pause_resume")] int PushbulletPrioPauseResume,
    [property: JsonPropertyName("pushbullet_prio_pp")] int PushbulletPrioPp,
    [property: JsonPropertyName("pushbullet_prio_complete")] int PushbulletPrioComplete,
    [property: JsonPropertyName("pushbullet_prio_failed")] int PushbulletPrioFailed,
    [property: JsonPropertyName("pushbullet_prio_disk_full")] int PushbulletPrioDiskFull,
    [property: JsonPropertyName("pushbullet_prio_new_login")] int PushbulletPrioNewLogin,
    [property: JsonPropertyName("pushbullet_prio_warning")] int PushbulletPrioWarning,
    [property: JsonPropertyName("pushbullet_prio_error")] int PushbulletPrioError,
    [property: JsonPropertyName("pushbullet_prio_queue_done")] int PushbulletPrioQueueDone,
    [property: JsonPropertyName("pushbullet_prio_other")] int PushbulletPrioOther
);

public record Pushover(
    [property: JsonPropertyName("pushover_token")] string PushoverToken,
    [property: JsonPropertyName("pushover_userkey")] string PushoverUserkey,
    [property: JsonPropertyName("pushover_device")] string PushoverDevice,
    [property: JsonPropertyName("pushover_emergency_expire")] int PushoverEmergencyExpire,
    [property: JsonPropertyName("pushover_emergency_retry")] int PushoverEmergencyRetry,
    [property: JsonPropertyName("pushover_enable")] int PushoverEnable,
    [property: JsonPropertyName("pushover_cats")] IReadOnlyList<string> PushoverCats,
    [property: JsonPropertyName("pushover_prio_startup")] int PushoverPrioStartup,
    [property: JsonPropertyName("pushover_prio_download")] int PushoverPrioDownload,
    [property: JsonPropertyName("pushover_prio_pause_resume")] int PushoverPrioPauseResume,
    [property: JsonPropertyName("pushover_prio_pp")] int PushoverPrioPp,
    [property: JsonPropertyName("pushover_prio_complete")] int PushoverPrioComplete,
    [property: JsonPropertyName("pushover_prio_failed")] int PushoverPrioFailed,
    [property: JsonPropertyName("pushover_prio_disk_full")] int PushoverPrioDiskFull,
    [property: JsonPropertyName("pushover_prio_new_login")] int PushoverPrioNewLogin,
    [property: JsonPropertyName("pushover_prio_warning")] int PushoverPrioWarning,
    [property: JsonPropertyName("pushover_prio_error")] int PushoverPrioError,
    [property: JsonPropertyName("pushover_prio_queue_done")] int PushoverPrioQueueDone,
    [property: JsonPropertyName("pushover_prio_other")] int PushoverPrioOther
);

public record Server(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("displayname")] string Displayname,
    [property: JsonPropertyName("host")] string Host,
    [property: JsonPropertyName("port")] int Port,
    [property: JsonPropertyName("timeout")] int Timeout,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("connections")] int Connections,
    [property: JsonPropertyName("ssl")] int Ssl,
    [property: JsonPropertyName("ssl_verify")] int SslVerify,
    [property: JsonPropertyName("ssl_ciphers")] string SslCiphers,
    [property: JsonPropertyName("enable")] int Enable,
    [property: JsonPropertyName("required")] int Required,
    [property: JsonPropertyName("optional")] int Optional,
    [property: JsonPropertyName("retention")] int Retention,
    [property: JsonPropertyName("expire_date")] string ExpireDate,
    [property: JsonPropertyName("quota")] string Quota,
    [property: JsonPropertyName("usage_at_start")] int UsageAtStart,
    [property: JsonPropertyName("priority")] int Priority,
    [property: JsonPropertyName("notes")] string Notes
);

