using ChatManager.Services;

namespace ChatManager.Forms
{
    public partial class ColorPickerForm : Form
    {
        public ColorPickerForm(string text, Color color)
        {
            InitializeComponent();
            Text = text;
            colorEditor.Color = color;
            Localize();
        }

        private string hexColor = string.Empty;
        public string GetHexColor => hexColor;

        private void ColorChanged(object sender, EventArgs e)
        {
            lblExample.ForeColor = colorEditor.Color;
        }

        private void ColorPickerForm_Load(object sender, EventArgs e)
        {
            Logging.Write(LogEvent.Info, ProgramClass.ColorPickerForm, $"ColorPickerForm is starting as: {Text}");
            Logging.Write(LogEvent.Variable, ProgramClass.ColorPickerForm, $"colorEditor.Color is: {colorEditor.Color}");
        }

        private void ColorPickerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logging.Write(LogEvent.Info, ProgramClass.ColorPickerForm, $"ColorPickerForm: {Text} is closing");
            Logging.Write(LogEvent.Variable, ProgramClass.ColorPickerForm, $"colorEditor.Color is: {colorEditor.Color}");
            hexColor = Converter.RGBtoHexAsync(colorEditor.Color);
        }

        private void ColorPickerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logging.Write(LogEvent.Info, ProgramClass.ColorPickerForm, $"ColorPickerForm: {Text} closed");
        }

        private void Localize()
        {
            Logging.Write(LogEvent.Method, ProgramClass.ColorPickerForm, "Localize entered");

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            lblExample.Text = localization.GetString(lblExample.Name);
            Logging.Write(LogEvent.Variable, ProgramClass.ColorPickerForm, $"lblExample.Text set to {lblExample.Text}");
        }
    }
}
