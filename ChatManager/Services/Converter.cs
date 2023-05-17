using System.Globalization;

namespace ChatManager.Services
{
    internal class Converter
    {
        // Convert an RGB Color into Hex
        public static async Task<string> RGBtoHexAsync(Color rgb)
        {
            await Logging.Write(LogEvent.Info, ProgramClass.Converter, "Try to convert RGB into Hex").ConfigureAwait(false);
            string hex = $"{rgb.R:X2}{rgb.G:X2}{rgb.B:X2}";
            await Logging.Write(LogEvent.Info, ProgramClass.Converter, $"Converted Hex is: {hex}").ConfigureAwait(false);

            return hex;
        }

        // Convert Hex into an RGB Color
        public static async Task<Color> HexToRGBAsync(string hex)
        {
            await Logging.Write(LogEvent.Info, ProgramClass.Converter, "Try to convert Hex into RGB").ConfigureAwait(false);

            if (hex.IndexOf("#") != -1)
            {
                hex = hex.Replace("#", "");
                await Logging.Write(LogEvent.Info, ProgramClass.Converter, "Trailing # removed.").ConfigureAwait(false);
            }

            // Set r g b to the correspodending values
            byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            await Logging.Write(LogEvent.Variable, ProgramClass.Converter, $"RGB is: {r}, {g}, {b}").ConfigureAwait(false);

            // Convert the r g b to Color
            Color rgb = Color.FromArgb(r, g, b);
            await Logging.Write(LogEvent.Variable, ProgramClass.Converter, $"Converted RGB is: {rgb}").ConfigureAwait(false);

            return rgb;
        }
    }
}
