using DataAccess.Core;
using Fclp;

namespace WPFCross.Startup.Services
{
    public class ArgsService : IArgsService
    {
        private readonly FluentCommandLineParser parser;

        public ArgsService()
        {
            parser = new FluentCommandLineParser();
            parser.Setup<bool>("test")
                .Callback(i => Test = i)
                .SetDefault(false);

            parser.Setup<DbAccessType>('t', "dbType")
                .Callback(i => DbAccessType = i)
                .SetDefault(DbAccessType.Mock);

            parser.Setup<bool>('d', "dummyData")
                .Callback(i => DummyData = i)
                .SetDefault(false);

            parser.Setup<bool>('c', "dbClear")
                .Callback(i => Clear = i)
                .SetDefault(false);
        }

        public bool Test { get; private set; }

        public DbAccessType DbAccessType { get; private set; }

        public bool DummyData { get; private set; }

        public bool Clear { get; private set; }

        public bool Parse(string[] args)
        {
            return !parser.Parse(args).HasErrors;
        }
    }
}
