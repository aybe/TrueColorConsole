using JetBrains.Annotations;

namespace TrueColorConsole
{

    /// <summary>
    /// Defines the character that is currently in use.
    /// </summary>
    [PublicAPI]
    public enum VTCharSet
    {
        /// <summary>
        /// Designate Character Set – US ASCII.
        /// </summary>
        Ascii,

        /// <summary>
        /// Designate Character Set – DEC Line Drawing.
        /// </summary>
        DecLineDrawing
    }
}