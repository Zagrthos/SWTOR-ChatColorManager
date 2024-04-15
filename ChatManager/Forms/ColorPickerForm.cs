using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using ChatManager.Enums;
using ChatManager.Services;

namespace ChatManager.Forms;

internal partial class ColorPickerForm : Form
{
    internal ColorPickerForm(string text, Color color)
    {
        InitializeComponent();
        Text = text;
        colorEditor.Color = color;
        nbFontSize.Value = 9;
        Localize();
    }

    internal string GetHexColor { get; private set; } = string.Empty;

    private void ColorChanged(object sender, EventArgs e) => lblExample.ForeColor = colorEditor.Color;

    private void ChangeFont(float fontSize)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.ColorPickerForm, "ChangeFont entered");
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.ColorPickerForm, $"FontSize: {fontSize}");

        PrivateFontCollection fontCollection = new();

        fontCollection.AddFontFile(Path.Combine(Application.StartupPath, "Resources", "Font.ttf"));

        Font swtorFont = new(fontCollection.Families[0], fontSize);

        lblExample.Font = swtorFont;
    }

    private void FontSizeChanged(object sender, EventArgs e) => ChangeFont((float)nbFontSize.Value);

    private void ColorPickerForm_Load(object sender, EventArgs e)
    {
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.ColorPickerForm, $"ColorPickerForm is starting as: {Text}");
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.ColorPickerForm, $"colorEditor.Color is: {colorEditor.Color}");
    }

    private void ColorPickerForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.ColorPickerForm, $"ColorPickerForm: {Text} is closing");
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.ColorPickerForm, $"colorEditor.Color is: {colorEditor.Color}");
        GetHexColor = Converter.RGBtoHexAsync(colorEditor.Color);
    }

    private void ColorPickerForm_FormClosed(object sender, FormClosedEventArgs e) => Logging.Write(LogEventEnum.Info, ProgramClassEnum.ColorPickerForm, $"ColorPickerForm: {Text} closed");

    private void Localize()
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.ColorPickerForm, "Localize entered");

        Localization localization = new(GetSetSettings.GetCurrentLocale);

        lblExample.Text = localization.GetString(lblExample.Name);
        lblFontSize.Text = localization.GetString(lblFontSize.Name);
    }
}
