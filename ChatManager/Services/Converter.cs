using System.Globalization;

namespace ChatManager.Services
{
    internal class Converter
    {
        // Convert an RGB Color into Hex
        public static string RGBtoHexAsync(Color rgb)
        {
            Logging.Write(LogEvent.Info, ProgramClass.Converter, "Try to convert RGB into Hex");
            string hex = $"{rgb.R:X2}{rgb.G:X2}{rgb.B:X2}";
            Logging.Write(LogEvent.Info, ProgramClass.Converter, $"Converted Hex is: {hex}");

            return hex;
        }

        // Convert Hex into an RGB Color
        public static Color HexToRGBAsync(string hex)
        {
            Logging.Write(LogEvent.Info, ProgramClass.Converter, "Try to convert Hex into RGB");

            if (hex.IndexOf("#") != -1)
            {
                hex = hex.Replace("#", "");
                Logging.Write(LogEvent.Info, ProgramClass.Converter, "Trailing # removed.");
            }

            // Set r g b to the correspodending values
            byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            Logging.Write(LogEvent.Variable, ProgramClass.Converter, $"RGB is: {r}, {g}, {b}");

            // Convert the r g b to Color
            Color rgb = Color.FromArgb(r, g, b);
            Logging.Write(LogEvent.Variable, ProgramClass.Converter, $"Converted RGB is: {rgb}");

            return rgb;
        }
    }
}
