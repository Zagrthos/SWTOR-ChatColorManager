using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using ChatManager.Enums;

namespace ChatManager.Services;

internal partial class Converter
{
    /// <summary>
    /// Convert an RGB <seealso cref="Color"/> into Hex.
    /// </summary>
    /// <param name="rgb">The <seealso cref="Color"/> to be converted.</param>
    /// <returns>The <see langword="string"/> in Hex.</returns>
    internal static string RGBtoHex(in Color rgb)
    {
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.Converter, "Try to convert RGB into Hex");
        string hex = $"{rgb.R:X2}{rgb.G:X2}{rgb.B:X2}";
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.Converter, $"Converted Hex is: {hex}");

        return hex;
    }

    /// <summary>
    /// Convert Hex into an RGB <seealso cref="Color"/>.
    /// </summary>
    /// <param name="hex">The <see langword="string"/> in Hex to be converted.</param>
    /// <returns>The <seealso cref="Color"/> in RGB.</returns>
    internal static Color HexToRGB(string hex)
    {
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.Converter, "Try to convert Hex into RGB");

        if (hex.Contains('#', StringComparison.OrdinalIgnoreCase))
        {
            hex = hex.Replace("#", string.Empty);
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Converter, "Trailing # removed.");
        }

        // Set r g b to the correspodending values
        byte r = byte.Parse(hex.AsSpan(0, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
        byte g = byte.Parse(hex.AsSpan(2, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
        byte b = byte.Parse(hex.AsSpan(4, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Converter, $"RGB is: {r}, {g}, {b}");

        // Convert the r g b to Color
        Color rgb = Color.FromArgb(r, g, b);
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Converter, $"Converted RGB is: {rgb}");

        return rgb;
    }

    /// <summary>
    /// Associate the server name to the SWTOR identifier.
    /// </summary>
    /// <param name="name">The server name as <see langword="string"/>.</param>
    /// <param name="isServerName"><see langword="true"/> if it's the server name, otherwise <see langword="false"/>.</param>
    /// <returns>The wanted <see langword="string"/>.</returns>
    internal static string ServerNameIdentifier(string name, bool isServerName)
    {
        if (!string.IsNullOrWhiteSpace(name) && isServerName)
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
        else if (!string.IsNullOrWhiteSpace(name) && !isServerName)
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

    internal static string AddWhitespace(string text) => AddWhiteSpaceRegex().Replace(text, " $1");
    internal static string RemoveWhitespace(string text) => text.Replace(" ", string.Empty);

    internal static string LabelToString(string text)
    {
        string[] splitted = text.Split(":");
        return splitted[1].Trim();
    }

    internal static double ConvertByteToMegabyte(long bytes) => Math.Round((double)bytes / (1024 * 1024), 2);

    // Regex for adding a whitespace
    [GeneratedRegex("(\\B[A-Z])")]
    private static partial Regex AddWhiteSpaceRegex();
}
