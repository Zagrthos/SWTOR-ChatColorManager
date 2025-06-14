; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

; Define some variables to use
#define MyAppName "SWTOR Chat Manager"
#define MyAppVersion "1.6.2"
#define MyAppPublisher "Zagrthos"
#define MyAppURL "https://github.com/Zagrthos/SWTOR-ChatColorManager"
#define MyAppSupportURL "https://github.com/Zagrthos/SWTOR-ChatColorManager/issues"
#define MyAppUpdateURL "https://github.com/Zagrthos/SWTOR-ChatColorManager/releases"
#define MyAppExeName "ChatManager.exe"
#define SetupVersion "1.6.2.0"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{375E2760-51D1-40D2-A6C6-D7F380E0CFB0}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppSupportURL}
AppUpdatesURL={#MyAppUpdateURL}
VersionInfoVersion={#SetupVersion}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
; Let the user decide if he wants to create an Icon or not
AllowNoIcons=yes
; Show install dir on ReadyToInstall Page
AlwaysShowDirOnReadyPage=yes
; Show Start Menu folder name on ReadyToInstall Page
AlwaysShowGroupOnReadyPage=yes
LicenseFile=C:\_InnoSetup\ChatManager\LICENSE.txt
; Let the user decide if it's installed in Programs or in LocalAppData
PrivilegesRequiredOverridesAllowed=dialog
OutputBaseFilename=SWTOR-ChatManager-v{#MyAppVersion}
SetupIconFile=C:\_InnoSetup\ChatManager\Logo.ico
Compression=lzma2/max
LZMAUseSeparateProcess=yes
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
MinVersion=10.0

[Languages]
Name: de; MessagesFile: "compiler:Languages\German.isl"
Name: en; MessagesFile: "compiler:Default.isl"
Name: fr; MessagesFile: "compiler:Languages\French.isl"

[CustomMessages]
de.DotNetFail = Fehler bei der Installation von .NET 8
de.DotNetDownloadFail = Fehler bei dem Download von .NET 8
de.DotNetInstall = .NET 8 wird installiert... 
de.DotNetInstallDesc = Bitte warte während .NET 8 auf deinem System installiert wird... 
de.DotNetInstalling = Installiere .NET 8... 
de.DotNetDownloadFailError = Fehler bei dem Download von .NET 8:
en.DotNetFail = Error while installing .NET 8
en.DotNetDownloadFail = Error while downloading .NET 8
en.DotNetInstall = .NET 8 is being installed... 
en.DotNetInstallDesc = Please wait while .NET 8 is being installed on your system... 
en.DotNetInstalling = Installing .NET 8... 
en.DotNetDownloadFailError = Error while downloading .NET 8:
fr.DotNetFail = Erreur lors de l'installation de .NET 8
fr.DotNetDownloadFail = Erreur lors du téléchargement de .NET 8
fr.DotNetInstall = .NET 8 est en cours d'installation... 
fr.DotNetInstallDesc = Merci de patienter pendant l'installation de .NET 8 sur ton système... 
fr.DotNetInstalling = Installation de .NET 8... 
fr.DotNetDownloadFailError = Erreur lors du téléchargement de .NET 8: 

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\_InnoSetup\ChatManagerExe\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\_InnoSetup\ChatManagerExe\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent unchecked

[InstallDelete]
Type: filesandordirs; Name: "{app}"

[UninstallDelete]
Type: filesandordirs; Name: "{app}"

[Code]
var
  DotNetRuntimeInstaller: string;
  DownloadPage: TDownloadWizardPage;
  DotNetInstallProgressPage: TOutputProgressWizardPage;

function CheckDotNetVersion: Boolean;
var
  ErrorCode: Integer;
  TempDir: String;
  BatchFilePath: String;
  OutputFilePath: String;
  Output: AnsiString;
begin
  Result := True;

  TempDir := ExpandConstant('{tmp}');
  BatchFilePath := TempDir + '\CheckDotNet.bat';
  OutputFilePath := TempDir + '\DotNetRuntimes.txt';

  SaveStringToFile(BatchFilePath, '@echo off' + #13#10 + 
    'dotnet --list-runtimes > ' + OutputFilePath, False);
  
  if Exec(ExpandConstant('{cmd}'), '/C ' + BatchFilePath, '', SW_HIDE, ewWaitUntilTerminated, ErrorCode) then
  begin
    LoadStringFromFile(OutputFilePath, Output);
    if (Pos('Microsoft.WindowsDesktop.App 8.0.7', Output) = 0) then 
    begin
      Result := False;
    end;
  end;
end;

procedure InstallDotNet();
var
  ResultCode: Integer;
begin
  if FileExists(DotNetRuntimeInstaller) then
  begin
    if not Exec(DotNetRuntimeInstaller, '/quiet /norestart', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then
    begin
      Log(Format('Failed to install .NET 8.0. Error code: %d', [ResultCode]));
      MsgBox(CustomMessage('DotNetFail'), mbError, MB_OK);
    end;
  end
  else
  begin
    Log(Format('Installer file does not exist: %s', [DotNetRuntimeInstaller]));
    MsgBox(CustomMessage('DotNetDownloadFail'), mbError, MB_OK);
  end;
end;

procedure InitializeWizard;
begin
  DotNetRuntimeInstaller := ExpandConstant('{tmp}\dotnet-installer.exe');
  DownloadPage := CreateDownloadPage(SetupMessage(msgWizardPreparing), SetupMessage(msgPreparingDesc), nil);
  DotNetInstallProgressPage := CreateOutputProgressPage(CustomMessage('DotNetInstall'), CustomMessage('DotNetInstallDesc'));
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
  if CurPageID = wpReady then begin
    if not CheckDotNetVersion then begin
      DownloadPage.Clear;
      DownloadPage.Add('https://builds.dotnet.microsoft.com/dotnet/WindowsDesktop/8.0.16/windowsdesktop-runtime-8.0.16-win-x64.exe', 'dotnet-installer.exe', '');
      DownloadPage.Show;
      try
        try
          DownloadPage.Download;
          DotNetRuntimeInstaller := ExpandConstant('{tmp}\dotnet-installer.exe');
          Log(Format('Downloaded .NET 8 installer to: %s', [DotNetRuntimeInstaller]));
          DownloadPage.Hide;

          DotNetInstallProgressPage.Show;
          DotNetInstallProgressPage.SetText(CustomMessage('DotNetInstalling'), '');
          InstallDotNet();
          DotNetInstallProgressPage.Hide;

          Result := True;
        except
          Log(Format('Failed to download .NET 8 installer: %s', [GetExceptionMessage]));
          MsgBox(CustomMessage('DotNetDownloadFailError') + GetExceptionMessage, mbError, MB_OK);
          Result := False;
        end;
      finally
        DownloadPage.Hide;
      end;
    end
    else
      Result := True;
  end else
    Result := True;
end;
