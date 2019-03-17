using DataAccess.Core;

namespace WPFCross.Startup.Services
{
    public interface IArgsService
    {
        DbAccessType DbAccessType { get; }
        bool DummyData { get; }
        bool Clear { get; }
        bool Test { get; }
    }
}
