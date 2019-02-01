using DataAccess.Core;

namespace WPFCross.UI.Services
{
    public interface IArgsService
    {
        DbAccessType DbAccessType { get; }
        bool DbInitialize { get; }
        bool Clear { get; }
    }
}
