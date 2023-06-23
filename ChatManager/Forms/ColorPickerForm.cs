using ChatManager.Enums;
using ChatManager.Services;

namespace ChatManager.Forms
{
    internal partial class ColorPickerForm : Form
    {
        internal ColorPickerForm(string text, Color color)
        {
            InitializeComponent();
            Text = text;
            colorEditor.Color = color;
            Localize();
        }

        private string hexColor = string.Empty;
        internal string GetHexColor => hexColor;

        private void ColorChanged(object sender, EventArgs e)
        {
            lblExample.ForeColor = colorEditor.Color;
        }

        private void ColorPickerForm_Load(object sender, EventArgs e)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.ColorPickerForm, $"ColorPickerForm is starting as: {Text}");
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.ColorPickerForm, $"colorEditor.Color is: {colorEditor.Color}");
        }

        private void ColorPickerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.ColorPickerForm, $"ColorPickerForm: {Text} is closing");
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.ColorPickerForm, $"colorEditor.Color is: {colorEditor.Color}");
            hexColor = Converter.RGBtoHexAsync(colorEditor.Color);
        }

        private void ColorPickerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.ColorPickerForm, $"ColorPickerForm: {Text} closed");
        }

        private void Localize()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.ColorPickerForm, "Localize entered");

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            lblExample.Text = localization.GetString(lblExample.Name);
        }
    }
}