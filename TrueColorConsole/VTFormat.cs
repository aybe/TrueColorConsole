using JetBrains.Annotations;

namespace TrueColorConsole
{
    /// <summary>
    ///     Defines formatting options.
    /// </summary>
    [PublicAPI]
    public enum VTFormat
    {
        /// <summary>
        ///     Returns all attributes to the default state prior to modification.
        /// </summary>
        Default = 0,

        /// <summary>
        ///     Applies brightness/intensity flag to foreground color.
        /// </summary>
        BoldBright = 1,

        /// <summary>
        ///     Adds underline.
        /// </summary>
        Underline = 4,

        /// <summary>
        ///     Removes underline.
        /// </summary>
        NoUnderline = 24,

        /// <summary>
        ///     Swaps foreground and background colors.
        /// </summary>
        Negative = 7,

        /// <summary>
        ///     Returns foreground/background to normal.
        /// </summary>
        Positive = 27,

        /// <summary>
        ///     Applies non-bold/bright black to foreground.
        /// </summary>
        ForegroundBlack = 30,

        /// <summary>
        ///     Applies non-bold/bright red to foreground.
        /// </summary>
        ForegroundRed = 31,

        /// <summary>
        ///     Applies non-bold/bright green to foreground.
        /// </summary>
        ForegroundGreen = 32,

        /// <summary>
        ///     Applies non-bold/bright yellow to foreground.
        /// </summary>
        ForegroundYellow = 33,

        /// <summary>
        ///     Applies non-bold/bright blue to foreground.
        /// </summary>
        ForegroundBlue = 34,

        /// <summary>
        ///     Applies non-bold/bright magenta to foreground.
        /// </summary>
        ForegroundMagenta = 35,

        /// <summary>
        ///     Applies non-bold/bright cyan to foreground.
        /// </summary>
        ForegroundCyan = 36,

        /// <summary>
        ///     Applies non-bold/bright white to foreground.
        /// </summary>
        ForegroundWhite = 37,

        // ForegroundExtended = 38, // NOTE using methods instead
        /// <summary>
        ///     Applies only the foreground portion of the defaults (see <see cref="Default" />).
        /// </summary>
        ForegroundDefault = 39,

        /// <summary>
        ///     Applies non-bold/bright black to background.
        /// </summary>
        BackgroundBlack = 40,

        /// <summary>
        ///     Applies non-bold/bright red to background.
        /// </summary>
        BackgroundRed = 41,

        /// <summary>
        ///     Applies non-bold/bright green to background.
        /// </summary>
        BackgroundGreen = 42,

        /// <summary>
        ///     Applies non-bold/bright yellow to background.
        /// </summary>
        BackgroundYellow = 43,

        /// <summary>
        ///     Applies non-bold/bright blue to background.
        /// </summary>
        BackgroundBlue = 44,

        /// <summary>
        ///     Applies non-bold/bright magenta to background.
        /// </summary>
        BackgroundMagenta = 45,

        /// <summary>
        ///     Applies non-bold/bright cyan to background.
        /// </summary>
        BackgroundCyan = 46,

        /// <summary>
        ///     Applies non-bold/bright white to background.
        /// </summary>
        BackgroundWhite = 47,

        // BackgroundExtended = 48, // NOTE using methods instead

        /// <summary>
        ///     Applies only the background portion of the defaults (see <see cref="Default" />).
        /// </summary>
        BackgroundDefault = 49,

        /// <summary>
        ///     Applies bold/bright black to foreground.
        /// </summary>
        BrightForegroundBlack = 90,

        /// <summary>
        ///     Applies bold/bright red to foreground.
        /// </summary>
        BrightForegroundRed = 91,

        /// <summary>
        ///     Applies bold/bright green to foreground.
        /// </summary>
        BrightForegroundGreen = 92,

        /// <summary>
        ///     Applies bold/bright yellow to foreground.
        /// </summary>
        BrightForegroundYellow = 93,

        /// <summary>
        ///     Applies bold/bright blue to foreground.
        /// </summary>
        BrightForegroundBlue = 94,

        /// <summary>
        ///     Applies bold/bright magenta to foreground.
        /// </summary>
        BrightForegroundMagenta = 95,

        /// <summary>
        ///     Applies bold/bright cyan to foreground.
        /// </summary>
        BrightForegroundCyan = 96,

        /// <summary>
        ///     Applies bold/bright white to foreground.
        /// </summary>
        BrightForegroundWhite = 97,

        /// <summary>
        ///     Applies bold/bright black to background.
        /// </summary>
        BrightBackgroundBlack = 100,

        /// <summary>
        ///     Applies bold/bright red to background.
        /// </summary>
        BrightBackgroundRed = 101,

        /// <summary>
        ///     Applies bold/bright green to background.
        /// </summary>
        BrightBackgroundGreen = 102,

        /// <summary>
        ///     Applies bold/bright yellow to background.
        /// </summary>
        BrightBackgroundYellow = 103,

        /// <summary>
        ///     Applies bold/bright blue to background.
        /// </summary>
        BrightBackgroundBlue = 104,

        /// <summary>
        ///     Applies bold/bright magenta to background.
        /// </summary>
        BrightBackgroundMagenta = 105,

        /// <summary>
        ///     Applies bold/bright cyan to background.
        /// </summary>
        BrightBackgroundCyan = 106,

        /// <summary>
        ///     Applies bold/bright white to background.
        /// </summary>
        BrightBackgroundWhite = 107
    }
}