﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using ChatManager.Enums;

namespace ChatManager.Services;

internal static partial class Converter
{
    /// <summary>
    /// Convert an RGB <seealso cref="Color"/> into Hex.
    /// </summary>
    /// <param name="rgb">The <seealso cref="Color"/> to be converted.</param>
    /// <returns>The <see langword="string"/> in Hex.</returns>
    internal static string RGBtoHex(in Color rgb)
    {
        Logging.Write(LogEvent.Info, LogClass.Converter, "Try to convert RGB into Hex");
        string hex = $"{rgb.R:X2}{rgb.G:X2}{rgb.B:X2}";
        Logging.Write(LogEvent.Info, LogClass.Converter, $"Converted Hex is: {hex}");

        return hex;
    }

    /// <summary>
    /// Convert Hex into an RGB <seealso cref="Color"/>.
    /// </summary>
    /// <param name="hex">The <see langword="string"/> in Hex to be converted.</param>
    /// <returns>The <seealso cref="Color"/> in RGB.</returns>
    internal static Color HexToRGB(string hex)
    {
        Logging.Write(LogEvent.Info, LogClass.Converter, "Try to convert Hex into RGB");

        if (hex.Contains('#', StringComparison.OrdinalIgnoreCase))
        {
            hex = hex.Replace("#", string.Empty, StringComparison.OrdinalIgnoreCase);
            Logging.Write(LogEvent.Info, LogClass.Converter, "Trailing # removed.");
        }

        // Set r g b to the correspodending values
        int r = int.Parse(hex.AsSpan(0, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
        int g = int.Parse(hex.AsSpan(2, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
        int b = int.Parse(hex.AsSpan(4, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
        Logging.Write(LogEvent.Variable, LogClass.Converter, $"RGB is: {r}, {g}, {b}");

        // Convert the r g b to Color
        Color rgb = Color.FromArgb(r, g, b);
        Logging.Write(LogEvent.Variable, LogClass.Converter, $"Converted RGB is: {rgb}");

        return rgb;
    }

    /// <summary>
    /// Associate the server name to the SWTOR identifier.
    /// </summary>
    /// <param name="name">The server name as <see langword="string"/>.</param>
    /// <param name="isServerName"><see langword="true"/> if it's the server name, otherwise <see langword="false"/>.</param>
    /// <returns>The wanted <see langword="string"/>.</returns>
    [SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "No nested ternaries.")]
    internal static string ServerNameIdentifier(string name, bool isServerName)
    {
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;

        return (isServerName) ? name switch
        {
            "StarForge" => "he3000",
            "SateleShan" => "he3001",
            "DarthMalgus" => "he4000",
            "TulakHord" => "he4001",
            "TheLeviathan" => "he4002",
            _ => string.Empty,
        } : name switch
        {
            "he3000" => "StarForge",
            "he3001" => "SateleShan",
            "he4000" => "DarthMalgus",
            "he4001" => "TulakHord",
            "he4002" => "TheLeviathan",
            _ => string.Empty,
        };
    }

    internal static string AddWhitespace(string text) => AddWhiteSpaceRegex().Replace(text, " $1");
    internal static string RemoveWhitespace(string text) => text.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase);

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
