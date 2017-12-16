using JetBrains.Annotations;

namespace TrueColorConsole
{
    [PublicAPI]
    public enum VTFormat
    {
        Default = 0,

        //BoldBright = 1,

        Underline = 4,
        NoUnderline = 24,
        Negative = 7,
        Positive = 27

        //ForegroundBlack = 30,
        //ForegroundRed = 31,
        //ForegroundGreen = 32,
        //ForegroundYellow = 33,
        //ForegroundBlue = 34,
        //ForegroundMagenta = 35,
        //ForegroundCyan = 36,
        //ForegroundWhite = 37,
        //ForegroundExtended = 38,
        //ForegroundDefault = 39,

        //BackgroundBlack = 40,
        //BackgroundRed = 41,
        //BackgroundGreen = 42,
        //BackgroundYellow = 43,
        //BackgroundBlue = 44,
        //BackgroundMagenta = 45,
        //BackgroundCyan = 46,
        //BackgroundWhite = 47,
        //BackgroundExtended = 48,
        //BackgroundDefault = 49,

        //BrightForegroundBlack = 90,
        //BrightForegroundRed = 91,
        //BrightForegroundGreen = 92,
        //BrightForegroundYellow = 93,
        //BrightForegroundBlue = 94,
        //BrightForegroundMagenta = 95,
        //BrightForegroundCyan = 96,
        //BrightForegroundWhite = 97,

        //BrightBackgroundBlack = 100,
        //BrightBackgroundRed = 101,
        //BrightBackgroundGreen = 102,
        //BrightBackgroundYellow = 103,
        //BrightBackgroundBlue = 104,
        //BrightBackgroundMagenta = 105,
        //BrightBackgroundCyan = 106,
        //BrightBackgroundWhite = 107
    }
}