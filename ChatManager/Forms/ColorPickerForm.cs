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
        }

        private string hexColor = string.Empty;
        public string GetHexColor => hexColor;

        private void ColorChanged(object sender, EventArgs e)
        {
            lblExample.ForeColor = colorEditor.Color;
        }

        private async void ColorPickerForm_Load(object sender, EventArgs e)
        {
            await Logging.Write(LogEvent.Info, ProgramClass.ColorPickerForm, $"ColorPickerForm is starting as: {Text}");
            await Logging.Write(LogEvent.Variable, ProgramClass.ColorPickerForm, $"colorEditor.Color is: {colorEditor.Color}");
        }

        private async void ColorPickerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await Logging.Write(LogEvent.Info, ProgramClass.ColorPickerForm, $"ColorPickerForm: {Text} is closing");
            await Logging.Write(LogEvent.Variable, ProgramClass.ColorPickerForm, $"colorEditor.Color is: {colorEditor.Color}");
            hexColor = await Converter.RGBtoHexAsync(colorEditor.Color);
        }

        private async void ColorPickerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            await Logging.Write(LogEvent.Info, ProgramClass.ColorPickerForm, $"ColorPickerForm: {Text} closed");
        }
    }
}
