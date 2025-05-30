namespace Socr.Utils;

/// <summary>
/// Extensions methods for <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Indicates whether the specified string is null or an empty string ("").
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>
    /// true if the value parameter is null or an empty string (""); otherwise, false.
    /// </returns>
    public static bool IsNullOrEmpty(this string? value) =>
        string.IsNullOrEmpty(value);

    /// <summary>
    /// Indicates whether the specified string is NOT null or NOT an empty string ("").
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>
    /// true if the value parameter is NOT null or NOT an empty string (""); otherwise, false.
    /// </returns>
    public static bool IsNotNullOrEmpty(this string? value) =>
        !string.IsNullOrEmpty(value);

    /// <summary>
    /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>
    /// true if the value parameter is null or Empty, or if value consists exclusively of white-space characters.
    /// </returns>
    public static bool IsNullOrWhiteSpace(this string? value) =>
        string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Indicates whether a specified string is NOT null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>
    /// true if the value parameter is NOT null or Empty, or if value consists exclusively of white-space characters.
    /// </returns>
    public static bool IsNotNullOrWhiteSpace(this string? value) =>
        !string.IsNullOrWhiteSpace(value);
}
