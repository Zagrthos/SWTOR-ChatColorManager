using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using ChatManager.Enums;
using ChatManager.Services;

namespace ChatManager.Forms;

internal sealed partial class ColorPickerForm : Form
{
    internal ColorPickerForm(string text, in Color color)
    {
        InitializeComponent();
        Text = text;
        colorEditor.Color = color;
        nbFontSize.Value = 9;
        Localize();
    }

    internal string GetHexColor { get; private set; } = string.Empty;

    private void ColorChanged(object sender, EventArgs e) => lblExample.ForeColor = colorEditor.Color;

    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Needed by WinForms.")]
    private void ChangeFont(float fontSize)
    {
        Logging.Write(LogEvent.Method, LogClass.ColorPickerForm, "ChangeFont entered");
        Logging.Write(LogEvent.Variable, LogClass.ColorPickerForm, $"FontSize: {fontSize}");

        PrivateFontCollection fontCollection = new();

        fontCollection.AddFontFile(Path.Combine(Application.StartupPath, "Resources", "Font.ttf"));

        lblExample.Font = new(fontCollection.Families[0], fontSize);
    }

    private void FontSizeChanged(object sender, EventArgs e) => ChangeFont((float)nbFontSize.Value);

    private void ColorPickerForm_Load(object sender, EventArgs e)
    {
        Logging.Write(LogEvent.Info, LogClass.ColorPickerForm, $"ColorPickerForm is starting as: {Text}");
        Logging.Write(LogEvent.Variable, LogClass.ColorPickerForm, $"colorEditor.Color is: {colorEditor.Color}");
    }

    private void ColorPickerForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Logging.Write(LogEvent.Info, LogClass.ColorPickerForm, $"ColorPickerForm: {Text} is closing");
        Logging.Write(LogEvent.Variable, LogClass.ColorPickerForm, $"colorEditor.Color is: {colorEditor.Color}");
        GetHexColor = Converter.RGBtoHex(colorEditor.Color);
    }

    private void ColorPickerForm_FormClosed(object sender, FormClosedEventArgs e) => Logging.Write(LogEvent.Info, LogClass.ColorPickerForm, $"ColorPickerForm: {Text} closed");

    private void Localize()
    {
        Logging.Write(LogEvent.Method, LogClass.ColorPickerForm, "Localize entered");

        Localization localization = new(GetSetSettings.GetCurrentLocale);

        lblExample.Text = localization.GetString(lblExample.Name);
        lblFontSize.Text = localization.GetString(lblFontSize.Name);
    }
}
