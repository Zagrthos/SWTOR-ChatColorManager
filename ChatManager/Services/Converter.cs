using ChatManager.Enums;
using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ChatManager.Services
{
    internal partial class Converter
    {
        // Convert an RGB Color into Hex
        internal static string RGBtoHexAsync(Color rgb)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Converter, "Try to convert RGB into Hex");
            string hex = $"{rgb.R:X2}{rgb.G:X2}{rgb.B:X2}";
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Converter, $"Converted Hex is: {hex}");

            return hex;
        }

        // Convert Hex into an RGB Color
        internal static Color HexToRGBAsync(string hex)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Converter, "Try to convert Hex into RGB");

            if (hex.IndexOf("#") != -1)
            {
                hex = hex.Replace("#", "");
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.Converter, "Trailing # removed.");
            }

            // Set r g b to the correspodending values
            byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Converter, $"RGB is: {r}, {g}, {b}");

            // Convert the r g b to Color
            Color rgb = Color.FromArgb(r, g, b);
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Converter, $"Converted RGB is: {rgb}");

            return rgb;
        }

        // Associate server name to identifier
        internal static string ServerNameIdentifier(string name, bool isServerName)
        {
            if (!string.IsNullOrEmpty(name) && isServerName)
            {
                return name switch
                {
                    "StarForge" => "he3000",
                    "SateleShan" => "he3001",
                    "DarthMalgus" => "he4000",
                    "TulakHord" => "he4001",
                    "TheLeviathan" => "he4002",
                    _ => string.Empty,
                };
            }
            else if (!string.IsNullOrEmpty(name) && !isServerName)
            {
                return name switch
                {
                    "he3000" => "StarForge",
                    "he3001" => "SateleShan",
                    "he4000" => "DarthMalgus",
                    "he4001" => "TulakHord",
                    "he4002" => "TheLeviathan",
                    _ => string.Empty,
                };
            }
            else
            {
                return string.Empty;
            }
        }

        internal static string AddWhitespace(string text)
        {
            return AddWhiteSpaceRegex().Replace(text, " $1");
        }

        internal static string RemoveWhitespace(string text)
        {
            return text.Replace(" ", "");
        }

        internal static string LabelToString(string text)
        {
            string[] splitted = text.Split(":");
            return splitted[1].Trim();
        }

        internal static double ConvertByteToMegabyte(long bytes)
        {
            return Math.Round((double)bytes / (1024 * 1024), 2);
        }

        // Regex for adding a whitespace
        [GeneratedRegex("(\\B[A-Z])")]
        private static partial Regex AddWhiteSpaceRegex();
    }
}