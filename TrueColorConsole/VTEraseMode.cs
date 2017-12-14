using JetBrains.Annotations;

namespace TrueColorConsole
{
    [PublicAPI]
    public enum VTEraseMode
    {
        FromBeginningToCursor = 0,
        FromCursorToEnd = 1,
        Entirely = 2
    }
}