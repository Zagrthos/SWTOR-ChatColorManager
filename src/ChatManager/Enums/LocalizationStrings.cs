using System.Diagnostics.CodeAnalysis;

namespace ChatManager.Enums;

[SuppressMessage("Roslynator", "RCS0036:Remove blank line between single-line declarations of same kind", Justification = "Code Style")]
[SuppressMessage("Roslynator", "RCS1181:Convert comment to documentation comment", Justification = "Comments are for clarity!")]
internal enum LocalizationStrings
{
    // MainForm Strings
    downloadProgressToolStripMenuItem,
    changelogToolStripMenuItem,

    // SettingsForm Strings
    UpdateIntervalOnStart,
    UpdateIntervalDaily,
    UpdateIntervalWeekly,

    // TextViewerForm Strings
    ChangelogFormName,
    ChangelogLabelName,
    ChangelogTryAgainLater,
    LicenceFormName,
    LicenceLabelName,

    // MessageBox Strings
    Err_AutosaveImport,
    Err_ColorNotHex,
    Err_NoFileToDeleteSelected,
    Err_NoExportFileSelected,
    Error_IsDetected,
    Inf_AutosaveImport,
    Inf_ColorsReset,
    Inf_ExportedFiles,
    Inf_NoFilesInBackupDir,
    Inf_RestartRequired,
    MessageBoxError,
    MessageBoxInfo,
    MessageBoxNoUpdate,
    MessageBoxUpdate,
    MessageBoxWarn,
    Question_MeteredConnection,
    Update_IsAvailable,
    Update_FileSize,
    Update_IsInstallReady,
    Update_IsNotAvailable,
    Warn_BackupDirMissing,
    Warn_NoImportFound,
    Warn_NoInternetConnection,
    Warn_SWTORpathNotFound,
    Warn_SWTORrunning,
    Warn_TextBoxEmpty
}
