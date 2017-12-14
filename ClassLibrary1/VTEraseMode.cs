using JetBrains.Annotations;

namespace ClassLibrary1
{
    [PublicAPI]
    public enum VTEraseMode
    {
        FromBeginningToCursor = 0,
        FromCursorToEnd = 1,
        Entirely = 2
    }
}