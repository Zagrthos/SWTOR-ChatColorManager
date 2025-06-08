using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChatManager.Enums;
using ChatManager.Services;

namespace ChatManager.Forms;

internal sealed partial class BackupSelectorForm : Form
{
    internal BackupSelectorForm()
    {
        InitializeComponent();
        Localize();
        DisplayBackupDirs();
    }

    private static readonly string BackupPath = GetSetSettings.GetBackupPath;
    private string[,] _filesInDir = new string[1000, 2];

    private void DisplayBackupDirs()
    {
        Logging.Write(LogEvent.Method, LogClass.BackupSelector, "DisplayBackupDirs entered");

        // Search the given Path for Directories
        string[] backupDirs = Directory.GetDirectories(BackupPath);
        Logging.Write(LogEvent.Variable, LogClass.BackupSelector, $"backupDirs: {backupDirs.Length}");

        // Create new Array with the size of the found Paths
        string[] backupDirsName = new string[backupDirs.Length];
        Logging.Write(LogEvent.Variable, LogClass.BackupSelector, $"backupDirsName: {backupDirsName.Length}");

        // Fill the Array with the Directory names
        for (int i = 0; i < backupDirs.Length; i++)
        {
            backupDirsName[i] = Path.GetFileName(backupDirs[i]);
            //Logging.Write(LogEventEnum.Variable, ProgramClassEnum.BackupSelector, backupDirsName[i]);
        }

        // Reverse the names so the newest is first
        Array.Reverse(backupDirsName);

        // Set the DataSource
        lbxBackupDir.DataSource = backupDirsName;
        btnDeleteDir.Enabled = backupDirsName.Length != 0;
    }

    private void DisplayBackupFiles(string dirName)
    {
        Logging.Write(LogEvent.Method, LogClass.BackupSelector, "DisplayBackupFiles entered");

        // Clear the DataSource so the checked Items get unchecked
        clbxBackupFiles.DataSource = null;

        // Search the given Path for Files
        string[] dirContent = Directory.GetFiles(dirName);
        Logging.Write(LogEvent.Variable, LogClass.BackupSelector, $"fileNames: {dirContent.Length}");

        if (dirContent.Length == 0)
        {
            btnDeleteFiles.Enabled = false;
            btnBackupDeselectAll.Enabled = false;
            btnBackupSelectAll.Enabled = false;
            btnRestore.Enabled = false;
            return;
        }

        btnDeleteFiles.Enabled = true;
        btnBackupDeselectAll.Enabled = true;
        btnBackupSelectAll.Enabled = true;
        btnRestore.Enabled = true;

        // Associate the Files with their Paths
        AssociateFilesWithPaths(dirContent);

        // Create new Array with the size of the found Files
        string[] files = new string[dirContent.Length];

        for (int i = 0; i < _filesInDir.Length / 2; i++)
        {
            //Logging.Write(LogEventEnum.Variable, ProgramClassEnum.BackupSelector, $"{i}");

            // Check if all parts in array are NOT empty or null
            if (!string.IsNullOrWhiteSpace(_filesInDir[i, 0]) && !string.IsNullOrWhiteSpace(_filesInDir[i, 1]) && !string.IsNullOrWhiteSpace(_filesInDir[i, 2]))
            {
                //Logging.Write(LogEventEnum.Variable, ProgramClassEnum.BackupSelector, $"{filesInDir[i, 0]} & {filesInDir[i, 1]} & {filesInDir[i, 2]}");
                //Logging.Write(LogEventEnum.Variable, ProgramClassEnum.BackupSelector, $"file[{i}]");

                // If not null or empty add the name and the server to the list
                files[i] = $"{_filesInDir[i, 0]} - {Converter.AddWhitespace(Converter.ServerNameIdentifier(_filesInDir[i, 1], false))}";
                Logging.Write(LogEvent.Variable, LogClass.BackupSelector, files[i]);
            }
            else
            {
                Logging.Write(LogEvent.Variable, LogClass.BackupSelector, "Already done or null");
                break;
            }
        }

        // Set the DataSource
        clbxBackupFiles.DataSource = files;
    }

    private void AssociateFilesWithPaths(string[] paths)
    {
        Logging.Write(LogEvent.Method, LogClass.BackupSelector, "AssociateFilesWithPaths entered");

        // Clear the Array
        _filesInDir = new string[1000, 3];

        // Now get the FileName, split it into parts and then
        // Set the name to pos 0
        // Set the server to pos 1
        // Set the path to pos 2
        for (int i = 0; i < paths.Length; i++)
        {
            string fileName = Path.GetFileName(paths[i]);
            string[] parts = fileName.Split("_");
            _filesInDir[i, 0] = parts[1];
            _filesInDir[i, 1] = parts[0];
            _filesInDir[i, 2] = paths[i];
            Logging.Write(LogEvent.Variable, LogClass.BackupSelector, _filesInDir[i, 0]);
            Logging.Write(LogEvent.Variable, LogClass.BackupSelector, _filesInDir[i, 1]);
            Logging.Write(LogEvent.Variable, LogClass.BackupSelector, _filesInDir[i, 2]);
        }
    }

    private void SelectBackupDir(object sender, EventArgs e)
    {
        Logging.Write(LogEvent.Method, LogClass.BackupSelector, "SelectBackupDir entered");

        if (sender is ListBox listBox)
        {
            string? dirName = listBox.SelectedItem?.ToString();

            if (!string.IsNullOrWhiteSpace(dirName))
            {
                DisplayBackupFiles(Path.Combine(BackupPath, dirName));
            }
            else
            {
                Logging.Write(LogEvent.Warning, LogClass.BackupSelector, "dirName is null or empty!");
            }
        }
        else
        {
            Logging.Write(LogEvent.Warning, LogClass.BackupSelector, $"Sender: {sender} is not a ListBox!");
        }
    }

    private void SelectClick(object sender, EventArgs e)
    {
        Logging.Write(LogEvent.Method, LogClass.BackupSelector, "SelectClick entered");

        if (sender is Button button)
        {
            bool isChecked = !button.Name.Contains("Deselect", StringComparison.OrdinalIgnoreCase);
            for (int i = 0; i < clbxBackupFiles.Items.Count; i++)
                clbxBackupFiles.SetItemChecked(i, isChecked);

            Logging.Write(LogEvent.Info, LogClass.BackupSelector, $"All Checked items are set to: {isChecked}");
        }
        else
        {
            Logging.Write(LogEvent.Warning, LogClass.BackupSelector, $"Sender: {sender} is not a Button!");
        }
    }

    private void DeleteClick(object sender, EventArgs e)
    {
        Logging.Write(LogEvent.Method, LogClass.BackupSelector, "DeleteClick entered");

        if (sender is Button button)
        {
            Logging.Write(LogEvent.Variable, LogClass.BackupSelector, $"Button is: {button.Name}");

            switch (button.Name)
            {
                case "btnDeleteDir":
                    if (!string.IsNullOrWhiteSpace(lbxBackupDir.SelectedItem!.ToString()))
                    {
                        Logging.Write(LogEvent.Variable, LogClass.BackupSelector, $"DirToDelete: {lbxBackupDir.SelectedItem}");

                        // Convert DataSource to List if not null
                        if (lbxBackupDir.DataSource is not null)
                        {
                            string[] array = (string[])lbxBackupDir.DataSource;
                            List<string> dataSource = [.. array];

                            // Delete the Directory
                            Directory.Delete(Path.Combine(BackupPath, lbxBackupDir.SelectedItem.ToString()!), true);

                            // Remove item from DataSource
                            dataSource.Remove(lbxBackupDir.SelectedItem.ToString()!);

                            // Set List as DataSource back in
                            lbxBackupDir.DataSource = dataSource.ToArray();

                            // If no items left disable buttons and clear ListBox
                            if (dataSource.Count == 0)
                            {
                                btnDeleteDir.Enabled = false;
                                btnDeleteFiles.Enabled = false;
                                clbxBackupFiles.DataSource = null;
                                btnBackupDeselectAll.Enabled = false;
                                btnBackupSelectAll.Enabled = false;
                                btnRestore.Enabled = false;
                            }
                        }
                        else
                        {
                            Logging.Write(LogEvent.Error, LogClass.BackupSelector, $"DataSource is missing from {lbxBackupDir.Name}!");
                            ShowMessageBox.ShowBug();
                            return;
                        }
                    }
                    else
                    {
                        Logging.Write(LogEvent.Warning, LogClass.BackupSelector, "SelectedItem is null or empty!");
                        ShowMessageBox.ShowBug();
                        return;
                    }

                    break;

                case "btnDeleteFiles":
                    if (clbxBackupFiles.CheckedItems.Count > 0)
                    {
                        if (clbxBackupFiles.DataSource is not null)
                        {
                            string[] array = (string[])clbxBackupFiles.DataSource;
                            List<string> dataSource = [.. array];

                            foreach (string item in clbxBackupFiles.CheckedItems)
                            {
                                Logging.Write(LogEvent.Variable, LogClass.BackupSelector, $"FileToDelete: {item}");

                                // Split the name in two parts
                                string[] parts = item.Split(" - ");

                                // Generate the fileName
                                string fileName = Converter.ServerNameIdentifier(Converter.RemoveWhitespace(parts[1]), true) + $"_{parts[0]}_PlayerGUIState.ini";

                                // Generate the filePath
                                string path = Path.Combine(BackupPath, lbxBackupDir.SelectedItem!.ToString()!, fileName);

                                // Remove item from DataSource
                                dataSource.Remove(item);

                                // Delete the file
                                File.Delete(path);
                            }

                            // Set List as DataSource back in
                            clbxBackupFiles.DataSource = dataSource.ToArray();

                            // Deselect everything else
                            btnBackupDeselectAll.PerformClick();

                            // If no items left disable buttons
                            if (dataSource.Count == 0)
                            {
                                btnDeleteFiles.Enabled = false;
                                btnBackupDeselectAll.Enabled = false;
                                btnBackupSelectAll.Enabled = false;
                                btnRestore.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        Services.Localization localization = new(GetSetSettings.GetCurrentLocale);

                        ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxError), localization.GetString(Enums.LocalizationStrings.Err_NoFileToDeleteSelected));
                        return;
                    }

                    break;

                default:
                    throw new InvalidOperationException($"{button.Name} was not expected!");
            }
        }
        else
        {
            Logging.Write(LogEvent.Warning, LogClass.BackupSelector, $"Sender: {sender} is not a Button!");
        }
    }

    private void Restore(object sender, EventArgs e)
    {
        Logging.Write(LogEvent.Method, LogClass.BackupSelector, "Restore entered");

        if (sender is Button button && button.Name == btnRestore.Name)
        {
            Services.Localization localization = new(GetSetSettings.GetCurrentLocale);

            if (clbxBackupFiles.CheckedItems.Count == 0)
            {
                ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxError), localization.GetString(Enums.LocalizationStrings.Err_NoExportFileSelected));
                return;
            }

            // Get all the checked items in an array
            string[] checkedItems = new string[clbxBackupFiles.CheckedItems.Count];
            for (int i = 0; i < checkedItems.Length; i++)
            {
                string? item = clbxBackupFiles.CheckedItems[i]?.ToString();

                checkedItems[i] = (!string.IsNullOrWhiteSpace(item)) ? item : throw new InvalidOperationException($"{nameof(item)}[{i}] is null!");
            }

            Logging.Write(LogEvent.Variable, LogClass.BackupSelector, $"checkedItems: {checkedItems.Length}");

            string localPath = GetSetSettings.GetLocalPath;

            // Now loop the array
            for (int i = 0; i < checkedItems.Length; i++)
            {
                // Split the name in two parts
                string[] parts = checkedItems[i].Split(" - ");

                // Generate the fileName
                string fileName = Converter.ServerNameIdentifier(Converter.RemoveWhitespace(parts[1]), true) + $"_{parts[0]}_PlayerGUIState.ini";

                // Generate the filePath
                string path = Path.Combine(BackupPath, lbxBackupDir.SelectedItem!.ToString()!, fileName);

                // Replace the file
                File.Copy(path, Path.Combine(localPath, fileName), true);
            }

            string changedFiles = localization.GetString(Enums.LocalizationStrings.Inf_ExportedFiles);
            changedFiles = changedFiles.Replace("FILECOUNT", checkedItems.Length.ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase);

            ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxInfo), changedFiles);
        }
        else
        {
            Logging.Write(LogEvent.Warning, LogClass.BackupSelector, $"Sender: {sender} is not a Button or {btnRestore.Name}!");
        }
    }

    private void Localize()
    {
        Logging.Write(LogEvent.Method, LogClass.BackupSelector, "Localize entered");

        Services.Localization localization = new(GetSetSettings.GetCurrentLocale);

        Text = localization.GetString(Name);

        static List<T> GetControls<T>(Control parent) where T : Control
        {
            List<T> controls = [];

            foreach (Control control in parent.Controls)
            {
                if (control is T typedControl)
                    controls.Add(typedControl);

                controls.AddRange(GetControls<T>(control));
            }

            return controls;
        }

        List<Button> buttons = GetControls<Button>(this);
        List<Label> labels = GetControls<Label>(this);

        foreach (Button button in buttons)
        {
            button.Text = localization.GetString(button.Name);
        }

        foreach (Label label in labels.Where(l => l.Name != lblDateConvertion.Name))
        {
            label.Text = localization.GetString(label.Name);
        }

        (string date, string time) = Services.Localization.GetLocalDateTime();

        lblDateConvertion.Text = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss} = {date} - {time}";
    }
}
